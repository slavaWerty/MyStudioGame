using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class EnemyInjector : MonoBehaviour
{
    [SerializeField] private EnemyConfig _config;
    [SerializeField] private Transform _playerTransform;
    [Space(20)]
    [field: SerializeField] private List<Enemy> _enemies = new();

    [Inject]
    public void Construct()
    {

        foreach (var enemy in _enemies)
        {
            enemy.Initzialize(_config, _playerTransform);
        }
    }
}

