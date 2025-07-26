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
        
        public abstract IBoardSpread Create();
        
        public abstract List<Vector3> Spread(Transform centerTr, bool isLocal);
    }
}