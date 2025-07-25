using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cf.CBoard
{
    public class BoardSlot : MonoBehaviour
    {
        [Header("Slot")]
        [SerializeField] private BoardSlot nextSlot;

        [Header("Event")]
        public UnityEvent onPassEvent;
        public UnityEvent onStopEvent;

        private IEnumerator _coMove;
        
        public BoardSlot GetMoveNext(ref int count, ref List<BoardSlot> slotList)
        {
            slotList.Add(this);
            
            if (!nextSlot)
            {
                return this;
            }
            
            --count;

            if (count < 0)
            {
                onStopEvent?.Invoke();

                return this;
            }
            
            onPassEvent?.Invoke();
            
            return nextSlot.GetMoveNext(ref count, ref slotList);
        }
    }
}
