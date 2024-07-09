using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : IDisposable
{
    private DataEnemy _dataEnemy;
    private IEnemyFactory _factory;
    private Queue<FarEnemy> _enemies;
    private Transform _container;
    private Pool _pool;

    public float TimeBetweenSpawnEnemy { get; set; } 

    public EnemySpawner(EnemyConfig config, IEnemyFactory factory, Transform container)
    {
        _enemies = new Queue<FarEnemy>();
        _container = container;
        _pool = new Pool();

        _dataEnemy = config.DataEnemy;
        _factory = factory;

        TimeBetweenSpawnEnemy = _dataEnemy.TimeBetweenSpawn;
    }

    public void Spawn(string path)
    {
        _pool.GetEnemy(path, _dataEnemy, _factory, _container);
    }

    public void Dispose()
    {
        foreach (FarEnemy enemy in _enemies)
        {
            enemy.Dead -= _pool.PoolObject;
        }
    }
}

