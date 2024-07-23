using Infentory;
using System;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    [SerializeField] private InfentoryService _service;

    private PlayerBuffs _playerBuffs;

    public PlayerBuffs PlayerBuffs => _playerBuffs;
    public event Action PlayerBuffsChanged;

    private void Start()
    {
        _playerBuffs = new PlayerBuffs();
    }

    private void OnRemovedItemToInfentory(RemoveToInfentoryResult obj)
    {
        var playerBuffHealth = _playerBuffs.HealthBuff;
        var playerBuffDamage = _playerBuffs.DamageBuff;

        var healthResult = playerBuffHealth - obj.PlayerBuffs.HealthBuff;
        var damageResult = playerBuffDamage - obj.PlayerBuffs.DamageBuff;

        if (healthResult < 0)
            healthResult = 0;

        if (damageResult < 0)
            damageResult = 0;

        _playerBuffs = new PlayerBuffs
        {
            HealthBuff = healthResult,
            DamageBuff = damageResult
        };

        PlayerBuffsChanged?.Invoke();
    }

    private void OnAddedItemToInfentory(AddItemsToInfentoryResult obj)
    {
        var playerBuffHealth = _playerBuffs.HealthBuff;
        var playerBuffDamage = _playerBuffs.DamageBuff;

        var healthResult = playerBuffHealth + obj.PlayerBuffs.HealthBuff;
        var damageResult = playerBuffDamage + obj.PlayerBuffs.DamageBuff;

        if (healthResult < 0)
            healthResult = 0;

        if (damageResult < 0)
            damageResult = 0;

        _playerBuffs = new PlayerBuffs
        {
            HealthBuff = healthResult,
            DamageBuff = damageResult
        };

        PlayerBuffsChanged?.Invoke();
    }

    private void OnEnable()
    {
        _service.AddedItemToInfentory += OnAddedItemToInfentory;
        _service.RemovedItemToInfentory += OnRemovedItemToInfentory;
    }
    private void OnDisable()
    {
        _service.AddedItemToInfentory -= OnAddedItemToInfentory;
        _service.RemovedItemToInfentory -= OnRemovedItemToInfentory;
    }
}
