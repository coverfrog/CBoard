using UnityEngine;

namespace Cf.CBoard
{
    public class Slot : MonoBehaviour
    {
        [Header("Debug View")]
        [SerializeField] private Slot mNextSlot;

        public void SetNextSlot(Slot slot)
        {
            mNextSlot = slot;
        }
    }
}