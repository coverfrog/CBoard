using UnityEngine;

namespace Cf.CBoard
{
    public abstract class BoardMove : ScriptableObject
    {
        public abstract void OnMoving(BoardHandler handler, Vector3 startPos, Vector3 endPos, float per);

        public abstract void OnMoveEnd(BoardHandler handler, Vector3 endPos);
    }
}