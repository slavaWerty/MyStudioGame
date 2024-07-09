using UnityEngine;

public class FarEnemyFactory : IEnemyFactory
{
    private Transform _playerTransform;
    private Vector3 _spawnPoint;

    public FarEnemyFactory(Movement playerTransform, Transform spawnPoint)
    {
        _playerTransform = playerTransform.gameObject.transform;
        _spawnPoint = spawnPoint.position;
    }

    public Enemy CreateEnemy(string path, DataEnemy data)
    {
        var prefap = Resources.Load<GameObject>(path);
        var go = GameObject.Instantiate(prefap);
        var enemy = go.AddComponent<FarEnemy>();
        go.transform.position = _spawnPoint;
        enemy.Initzialize(data, _playerTransform);
        return enemy;
    }
}

