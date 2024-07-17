using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Queue<GameObject> _gameObjects;

    public Queue<GameObject> GameObjects => _gameObjects;

    public Pool()
    {
        _gameObjects = new Queue<GameObject>();
    }

    public IEnumerator PoolObjectWithTime(GameObject gameObject, float duration, Transform container)
    {
        WaitForSeconds wait = new WaitForSeconds(duration);

        yield return wait;

        _gameObjects.Enqueue(gameObject);

        if (container != null)
        {
            gameObject.gameObject.SetActive(false);

            if (gameObject.TryGetComponent(out BaseBullet bullet))
                bullet.RestartDirection();
        }
    }

    public GameObject GetObject(GameObject prefap)
    {
        if (_gameObjects.Count == 0)
        {
            var go = GameObject.Instantiate(prefap);
            return go;
        }

        _gameObjects.Peek().SetActive(true);
        return _gameObjects.Dequeue();
    }

    public Enemy GetEnemy(string path, DataEnemy data, IEnemyFactory factory, Transform container)
    {
        if (_gameObjects.Count == 0)
        {
            var enemy = factory.CreateEnemy(path, data);

            if (enemy.TryGetComponent(out IDamagable damagble))
                damagble.Dead += PoolObject;

            enemy.transform.parent = container;
            return enemy;
        }

        _gameObjects.Peek().gameObject.SetActive(true);
        return _gameObjects.Dequeue().GetComponent<FarEnemy>();
    }

    public void PoolObject(GameObject enemy)
    {
        enemy.SetActive(false);

        _gameObjects.Enqueue(enemy);
    }
}

