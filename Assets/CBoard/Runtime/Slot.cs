using UnityEngine;

namespace Cf.CBoard
{
    public class Slot : MonoBehaviour
    {
        [Header("Debug View")]
        [SerializeField] private Slot nextSlot;

        public void SetNextSlot(Slot slot)
        {
            nextSlot = slot;
        }
    }
}