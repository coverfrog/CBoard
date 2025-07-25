using UnityEngine;

namespace Cf.CBoard
{
    [CreateAssetMenu(menuName = "Cf/CBoard/Simple", fileName = "Simple")]
    public sealed class BoardMoveSimple : BoardMove
    {
        public override void OnMoving(BoardHandler handler, Vector3 startPos, Vector3 endPos, float per)
        {
            handler.UnitTr.position = Vector3.Lerp(startPos, endPos, per);
            
        }

        public override void OnMoveEnd(BoardHandler handler, Vector3 endPos)
        {
            handler.UnitTr.position = endPos;
            
        }
    }
}