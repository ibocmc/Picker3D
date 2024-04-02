using System.Collections.Generic;
using Data.ValueObjects;
using DG.Tweening;
using Runtime.Data.UnityObjects;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        [SerializeField] private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>();
        [SerializeField] private TextMeshPro poolText;
        [SerializeField] private byte stageID;
        [SerializeField] private Renderer renderer;


        [ShowInInspector] private PoolData _poolData;
        private LevelData _levelData;
        private readonly string _collectable="Collectable";
        [ShowInInspector] private byte _collectedCount;


        private void Awake()
        {
            _poolData = GetPoolData();

        }

        private PoolData GetPoolData()
        {
           return Resources.Load<CD_Level>("Data/CD_Level")
               .Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].Pools[stageID];
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
           CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateTweens;
           CoreGameSignals.Instance.onStageAreaSuccessful += OnChangePoolColor;
        }

        private void OnChangePoolColor(byte stageValue)
        {
            if(stageValue != stageID) return;

            renderer.material.DOColor(_levelData._color, .5f).SetEase(Ease.Flash).SetRelative(false);

        }

        private void OnActivateTweens(byte stageValue)
        {
           if(stageValue != stageID) return;
           
               foreach (var tween in tweens)
               {
                   tween.DOPlay();
               }
           
        }

        private void Start()
        {
            SetRequiredAmountText();
        }

        private void SetRequiredAmountText()
        {
           poolText.text = $"0/{_poolData.RequiredObjectCount}";
        }

        public bool TakeResult(byte managerStageValue)
        {
            if (stageID == managerStageValue)
            {
                return _collectedCount >= _poolData.RequiredObjectCount;
            }

            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag(_collectable)) return;
            IncreaseCollectedAmount();
            SetCollectedAmountToPool();
        }

        private void SetCollectedAmountToPool()
        {
            poolText.text = $"{_collectedCount}/{_poolData.RequiredObjectCount}";
        }

        private void IncreaseCollectedAmount()
        {
            _collectedCount++;
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.CompareTag(_collectable)) return;
            DecreaseCollectedAmount();
            SetCollectedAmountToPool();
        }

        private void DecreaseCollectedAmount()
        {
            _collectedCount--;
        }
    }
}