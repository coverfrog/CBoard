using System;
using System.Collections.Generic;
using Cf.CBoard.Spread;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Cf.CBoard
{
    public class Board : MonoBehaviour
    {
        [Header("Gizmo")]
        [SerializeField] private bool mIsDrawGizmos = true;
        [SerializeField][Min(0.01f)] private float mGizmoRadius = 0.1f;
        [SerializeField] private Color mGizmoColor = Color.white;

        [Header("Init")] [SerializeField] private bool mIsInitStart = true;
        [SerializeField] private bool mIsInitObject = true;
        
        [Header("Addressable")]
        [SerializeField] private AssetReference mSlotReference;
        
        [Header("Func")]
        [SerializeField] private BoardSpread mBoardSpread;

        [Header("Debug View")]
        [SerializeField] private Slot mSlotPrefab;
        [SerializeField] private List<Slot> mSlotList = new List<Slot>();
        [SerializeField] private List<Vector3> mPositionList = new List<Vector3>();

        public IReadOnlyList<Slot> SlotList => mSlotList;
        public IReadOnlyList<Vector3> PositionList => mPositionList;
        
        private void Start()
        {
            if (!mIsInitStart)
            {
                return;
            }
            
            Init(null);
        }

        public void Init(Action onComplete)
        {
            if (mBoardSpread == null)
            {
#if UNITY_EDITOR
                Debug.LogError("BoardSpread is null");
#endif
                return;
            }

            InitPositionList();

            if (!mIsInitObject)
            {
                onComplete?.Invoke();
                return;
            }
            
            InitPrefab(onComplete);
        }

        private void InitPositionList()
        {
            mPositionList = mBoardSpread.Clone().Spread(transform, true);
        }
        
        private void InitPrefab(Action onComplete)
        {
            if (!mIsInitObject)
            {
                return;
            }

            bool exist = mSlotReference != null && mSlotReference.RuntimeKeyIsValid();

            if (!exist)
            {
#if UNITY_EDITOR
                Debug.LogError("Slot reference is null");
#endif
                return;
            }

            Addressables.LoadAssetAsync<GameObject>(mSlotReference).Completed += (op) =>
            {
                if (op.Status != AsyncOperationStatus.Succeeded)
                {
                    return;
                }
          
                if (op.Result.TryGetComponent(out mSlotPrefab))
                {
                    InitObjects(onComplete);
                }

                else
                {
#if UNITY_EDITOR
                    Debug.LogError("Slot prefab is Not Have Slot Component");
#endif
                }
            };
        }

        private void InitObjects(Action onComplete)
        {
            if (!mSlotPrefab)
            {
#if UNITY_EDITOR
                Debug.LogError("Slot prefab is null");
#endif
                return;
            }
            
            mSlotList.Clear();
            
            int count = mPositionList.Count;
            
            for (int i = 0; i < count; i++)
            {
                Slot slot = Instantiate(mSlotPrefab, mPositionList[i], mSlotPrefab.transform.rotation, transform);
                slot.gameObject.name = $"Slot {i}";
                
                mSlotList.Add(slot);
            }
            
            for (int i = 0; i < count; i++)
            {
                Slot slot0 = mSlotList[i];
                Slot slot1 = mSlotList[(i + 1) % count];
                
                slot0.SetNextSlot(slot1);
            }
            
            onComplete?.Invoke();
        }
        
        private void OnDrawGizmos()
        {
            if (!mIsDrawGizmos)
            {
                return;
            }

            if (!mBoardSpread)
            {
                return;
            }
            
            var list = mBoardSpread.Clone().Spread(transform, false);
            
            Gizmos.color = mGizmoColor;
            
            foreach (Vector3 position in list)
            {
                Gizmos.DrawSphere(position, mGizmoRadius);
            }
        }
    }
}
