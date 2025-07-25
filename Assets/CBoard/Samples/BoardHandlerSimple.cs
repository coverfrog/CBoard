using Cf.CBoard;
using UnityEngine;

namespace CBoard.CBoard.Samples
{
    public class BoardHandlerSimple : BoardHandler
    {
        protected override void OnMoving(Vector3 startPos, Vector3 endPos, float per)
        {
            unitTr.position = Vector3.Lerp(startPos, endPos, per);
        }

        protected override void OnMoveEnd(Vector3 endPos)
        {
            unitTr.position = endPos;
        }
    }
}