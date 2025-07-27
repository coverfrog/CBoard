using System.Collections.Generic;
using UnityEngine;

namespace Cf.CBoard.Spread
{
    [CreateAssetMenu(menuName = "Cf/CBoard/Spread/Rectangle", fileName = "Rectangle")]
    public class BoardSpreadRectangle : BoardSpread
    {
        [SerializeField] private Axis mAxis;
        [Space]
        [SerializeField][Min(1)] private uint mCountWidth = 5;
        [SerializeField][Min(1)] private uint mCountHeight = 4;
        [Space]
        [SerializeField][Min(0)] private float mInterWidth = 2.0f;
        [SerializeField][Min(0)] private float mInterHeight = 2.0f;
        
        #region Property

        public Axis GetAxis => mAxis;
        public int GetCountWidth => (int)mCountWidth;
        public int GetCountHeight => (int)mCountHeight;
        public int GetInterWidth => (int)mInterWidth;
        public int GetInterHeight => (int)mInterHeight;

        #endregion
        
        public override IBoardSpread Clone()
        {
            var instance = CreateInstance<BoardSpreadRectangle>();
            instance.mAxis = mAxis;
            
            instance.mCountWidth = mCountWidth;
            instance.mCountHeight = mCountHeight;
            
            instance.mInterWidth = mInterWidth;
            instance.mInterHeight = mInterHeight;

            return instance;
        }

        public override List<Vector3> Spread(Transform origin, bool isLocal)
        {
            var result = new List<Vector3>();

            Vector3 center = isLocal ? origin.localPosition : origin.position;

            Vector3 leftBottom = center;
            
            leftBottom += Vector3.left * mCountWidth * mInterWidth * 0.5f;
            leftBottom += Vector3.right * mInterWidth * 0.5f;

            Vector3 offsetDirection = mAxis == Axis.XZ ? Vector3.forward : Vector3.up;
            
            leftBottom += offsetDirection * -1.0f * mCountHeight * mInterHeight * 0.5f;
            leftBottom += offsetDirection * mInterHeight * 0.5f;
            
            for (int y = 0; y < mCountHeight; y++)
            {
                for (int x = 0; x < mCountWidth; x++)
                {
                    if (x != 0 && x != mCountWidth - 1 && y != 0 && y != mCountHeight - 1)
                    {
                        continue;
                    }
                    
                    Vector3 position = leftBottom;
                    position += Vector3.right * x * mInterWidth;
                    position += offsetDirection * y * mInterHeight;
                    
                    result.Add(position);
                }
            }
            
            return result;
        }
    }
}