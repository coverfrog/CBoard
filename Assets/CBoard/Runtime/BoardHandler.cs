using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cf.CBoard
{
    public class BoardHandler : MonoBehaviour
    {
        [Header("Slot")]
        [SerializeField] private BoardSlot slotCurrent;
        [SerializeField] private List<BoardSlot> slotMoveList;
    
        [Header("Unit")]
        [SerializeField] private Transform unitTr;
        [SerializeField] private Animator unitAnimator;

        public Transform UnitTr => unitTr;
        public Animator UnitAnimator => unitAnimator;
        
        [Header("Move")]
        [SerializeField] private BoardMove move;
        [SerializeField] private int moveCount = 1;
        [SerializeField] private float moveDuration = 0.2f;
        
        private IEnumerator _coMove;
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Move();
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
            
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
            
            
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
            
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
            
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
            
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
            
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
            
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
            
            }
        }

        #region Move

        public void Move()
        {
            Move(moveCount);
        }

        public void Move(int count)
        {
            moveCount = count;
            
            slotMoveList.Clear();
            slotCurrent = slotCurrent.GetMoveNext(ref count, ref slotMoveList);
        
            _coMove = CoMove();
            StartCoroutine(_coMove);
        }

        private IEnumerator CoMove()
        {
            float dur = moveDuration;

            for (int i = 0; i < slotMoveList.Count - 1; i++)
            {
                yield return CoMove(slotMoveList[i].transform.position, slotMoveList[i + 1].transform.position, dur);

                if (i == slotMoveList.Count - 1)
                {
                    slotMoveList[i + 1].onStopEvent?.Invoke();
                }

                else if (i > 0)
                {
                    slotMoveList[i + 1].onPassEvent?.Invoke();
                }
            }
        }

        private IEnumerator CoMove(Vector3 startPos, Vector3 endPos, float dur)
        {
            for (float t = 0; t <= dur; t += Time.deltaTime)
            {
                float per = t / dur;

                move.OnMoving(this, startPos, endPos, per);
            
                yield return null;
            }

            move.OnMoveEnd(this, endPos);
        }

        #endregion
    }
}
