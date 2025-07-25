using UnityEngine;

namespace Cf.CBoard.Spread
{
    public abstract class BoardSpread : ScriptableObject
    {
        public abstract void Spread(BoardHandler handler);
    }
}