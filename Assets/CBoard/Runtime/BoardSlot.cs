using UnityEngine;

namespace Cf.CBoard
{
    public enum BordSlotType
    {
        PassBy,
        StartBy,
    }
    
    public class BoardSlot : MonoBehaviour
    {
        [Header("Bool")]
        [SerializeField] private bool isInUnit;
        
        public bool IsInUnit => isInUnit;
        
        [Header("Slot")]
        [SerializeField] private BoardSlot passBySlot;
        [SerializeField] private BoardSlot startBySlot;
        
        public BoardSlot PassBySlot => passBySlot;
        
        public BoardSlot StartBySlot => startBySlot;

        public void SetSlot(BordSlotType slotType, BoardSlot boardSlot)
        {
            if (slotType == BordSlotType.PassBy)
            {
                passBySlot = boardSlot;
            }

            else
            {
                startBySlot = boardSlot;
            }
        }

        public void SetSlot(BoardSlot boardSlot)
        {
            passBySlot = startBySlot = boardSlot;
        }

        public void SetIsInUnit(bool value)
        {
            isInUnit = value;
        }

        public bool GetNext(out BoardSlot  nextSlot)
        {
            nextSlot = isInUnit ? startBySlot : passBySlot;
            
            return nextSlot != null;;
        }
    }
}
