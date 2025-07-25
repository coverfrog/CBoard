using UnityEngine;

namespace Cf.CBoard.Spread
{
    [CreateAssetMenu(menuName = "Cf/CBoard/Spread/Circle", fileName = "Circle")]
    public sealed class BoardSpreadCircle : BoardSpread
    {
        public override void Spread(BoardHandler handler)
        {
            const float tau = Mathf.PI * 2;
            
            int childCount = handler.SlotParent.childCount;
            float interval = 1.0f / childCount;
            
            for (int i = 0; i < childCount; i++)
            {
                float y = interval * i;
                float a = tau * y;
                
                float x = Mathf.Cos(a);
                float z = Mathf.Sin(a);

                Transform tr = handler.SlotParent.GetChild(i);
                
                tr.position = new Vector3(x, 0, z) * handler.CircleRadius;
                tr.eulerAngles = new Vector3(0, Mathf.Atan2(x, z) * Mathf.Rad2Deg, 0);
            }
        }
    }
}