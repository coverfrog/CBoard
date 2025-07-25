using System.Collections.Generic;
using UnityEngine;

namespace Cf.CBoard
{
    public class BoardHandler : MonoBehaviour
    {
        [SerializeField] private BoardSlot beginSlot;
        
        public void Begin()
        {
            beginSlot.SetIsInUnit(true);
        }
    }
}
