using System.Collections.Generic;
using UnityEngine;

namespace Cf.CBoard.Spread
{
    public abstract class BoardSpread : ScriptableObject, IBoardSpread
    {
        public enum Axis
        {
            XZ,
            XY,
        }
        
        public abstract IBoardSpread Clone();
        
        public abstract List<Vector3> Spread(Transform origin, bool isLocal);
    }
}