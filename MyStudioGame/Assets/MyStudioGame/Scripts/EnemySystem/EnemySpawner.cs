using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : IDisposable
{
    private Transform _spawnPoint;
    private DataEnemy _dataEnemy;
    private IEnemyFactory _factory;
    private Queue<Enemy> _enemies;

    public float TimeBetweenSpawnEnemy { get; set; } 

    public EnemySpawner(EnemyConfig config, Transform spawnPoint, IEnemyFactory factory)
    {
        _enemies = new Queue<Enemy>();

        _dataEnemy = config.DataEnemy;
        _spawnPoint = spawnPoint;
        _factory = factory;

        TimeBetweenSpawnEnemy = _dataEnemy.TimeBetweenSpawn;
    }

    public void Spawn(string path)
    {
        var enemy = GetEnemy(path);

        enemy.transform.position = _spawnPoint.position;
    }

    public Enemy GetEnemy(string path)
    {
        if (_enemies.Count == 0)
        {
            var enemy = _factory.CreateEnemy(path, _dataEnemy);
            enemy.Dead += PoolEnemy;
            enemy.transform.parent = _spawnPoint;
            return enemy;
        }

        _enemies.Peek().gameObject.SetActive(true);
        return _enemies.Dequeue();
    }

    public void PoolEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        _enemies.Enqueue(enemy.GetComponent<Enemy>());
    }

    public void Dispose()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Dead -= PoolEnemy;
        }
    }
}

