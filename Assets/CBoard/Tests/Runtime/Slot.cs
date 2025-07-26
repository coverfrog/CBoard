using UnityEngine;

namespace Cf.CBoard.Tests
{
    public class Slot : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] private Slot nextSlot;

        public void SetNextSlot(Slot slot)
        {
            nextSlot = slot;
        }
    }
}