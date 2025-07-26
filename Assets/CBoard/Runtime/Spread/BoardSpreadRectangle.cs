using System.Collections.Generic;
using UnityEngine;

namespace Cf.CBoard.Spread
{
    [CreateAssetMenu(menuName = "Cf/CBoard/Spread/Rectangle", fileName = "Rectangle")]
    public class BoardSpreadRectangle : BoardSpread
    {
        [SerializeField] private Axis axis;
        [Space]
        [SerializeField][Min(1)] private uint countWidth = 5;
        [SerializeField][Min(1)] private uint countHeight = 4;
        [Space]
        [SerializeField][Min(0)] private float interWidth = 1.0f;
        [SerializeField][Min(0)] private float interHeight = 1.0f;
        
        #region Property

        public Axis GetAxis => axis;
        public int GetCountWidth => (int)countWidth;
        public int GetCountHeight => (int)countHeight;
        public int GetInterWidth => (int)interWidth;
        public int GetInterHeight => (int)interHeight;

        #endregion
        
        public override IBoardSpread Create()
        {
            var instance = CreateInstance<BoardSpreadRectangle>();
            instance.axis = axis;
            
            instance.countWidth = countWidth;
            instance.countHeight = countHeight;
            
            instance.interWidth = interWidth;
            instance.interHeight = interHeight;

            return instance;
        }

        public override List<Vector3> Spread(Transform centerTr, bool isLocal)
        {
            var result = new List<Vector3>();

            Vector3 center = isLocal ? centerTr.localPosition : centerTr.position;

            Vector3 leftBottom = center;
            
            leftBottom += Vector3.left * countWidth * interWidth * 0.5f;
            leftBottom += Vector3.right * interWidth * 0.5f;

            Vector3 offsetDirection = axis == Axis.XZ ? Vector3.forward : Vector3.up;
            
            leftBottom += offsetDirection * -1.0f * countHeight * interHeight * 0.5f;
            leftBottom += offsetDirection * interHeight * 0.5f;
            
            for (int y = 0; y < countHeight; y++)
            {
                for (int x = 0; x < countWidth; x++)
                {
                    if (x != 0 && x != countWidth - 1 && y != 0 && y != countHeight - 1)
                    {
                        continue;
                    }
                    
                    Vector3 position = leftBottom;
                    position += Vector3.right * x * interWidth;
                    position += offsetDirection * y * interHeight;
                    
                    result.Add(position);
                }
            }
            
            return result;
        }
    }
}