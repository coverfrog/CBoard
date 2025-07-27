using System.Collections.Generic;
using UnityEngine;

namespace Cf.CBoard.Spread
{
    public interface IBoardSpread
    {
        List<Vector3> Spread(Transform origin, bool isLocal);
    }
}