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
        [SerializeField] private bool isDrawGizmos;
        [SerializeField][Min(0.01f)] private float gizmoRadius = 0.1f;
        [SerializeField] private Color gizmoColor = Color.white;

        [Header("Init")] 
        [SerializeField] private bool isInitStart;
        [SerializeField] private bool isInitObject;
        
        [Header("Addressable")]
        [SerializeField] private AssetReference slotReference;
        
        [Header("Func")]
        [SerializeField] private BoardSpread boardSpread;

        [Header("Debug View")]
        [SerializeField] private Slot slotPrefab;
        [SerializeField] private List<Slot> slotList = new List<Slot>();
        [SerializeField] private List<Vector3> positionList = new List<Vector3>();

        public IReadOnlyList<Slot> GetSlotList => slotList;
        public IReadOnlyList<Vector3> GetPositionList => positionList;
        
        private void Start()
        {
            if (!isInitStart)
            {
                return;
            }
            
            Init();
        }

        public void Init()
        {
            if (boardSpread == null)
            {
#if UNITY_EDITOR
                Debug.LogError("BoardSpread is null");
#endif
                return;
            }

            InitPositionList();
            InitPrefab();
        }

        private void InitPositionList()
        {
            positionList = boardSpread.Create().Spread(transform, true);
        }
        
        private void InitPrefab()
        {
            if (!isInitObject)
            {
                return;
            }

            bool exist = slotReference != null && slotReference.RuntimeKeyIsValid();

            if (!exist)
            {
#if UNITY_EDITOR
                Debug.LogError("Slot reference is null");
#endif
                return;
            }

            Addressables.LoadAssetAsync<GameObject>(slotReference).Completed += (op) =>
            {
                if (op.Status != AsyncOperationStatus.Succeeded)
                {
                    return;
                }
          
                if (op.Result.TryGetComponent(out slotPrefab))
                {
                    InitObjects();
                }

                else
                {
#if UNITY_EDITOR
                    Debug.LogError("Slot prefab is Not Have Slot Component");
#endif
                }
            };
        }

        private void InitObjects()
        {
            if (!slotPrefab)
            {
#if UNITY_EDITOR
                Debug.LogError("Slot prefab is null");
#endif
                return;
            }
            
            slotList.Clear();
            
            int count = positionList.Count;
            
            for (int i = 0; i < count; i++)
            {
                Slot slot = Instantiate(slotPrefab, positionList[i], slotPrefab.transform.rotation, transform);
                slot.gameObject.name = $"Slot {i}";
                
                slotList.Add(slot);
            }
            
            for (int i = 0; i < count; i++)
            {
                Slot slot0 = slotList[i];
                Slot slot1 = slotList[(i + 1) % count];
                
                slot0.SetNextSlot(slot1);
            }
        }
        
        private void OnDrawGizmos()
        {
            if (!isDrawGizmos)
            {
                return;
            }

            if (!boardSpread)
            {
                return;
            }
            
            var list = boardSpread.Create().Spread(transform, false);
            
            Gizmos.color = gizmoColor;
            
            foreach (Vector3 position in list)
            {
                Gizmos.DrawSphere(position, gizmoRadius);
            }
        }
    }
}
