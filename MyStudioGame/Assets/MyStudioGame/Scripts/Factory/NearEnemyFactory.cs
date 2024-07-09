using UnityEngine;

public class NearEnemyFactory : IEnemyFactory
{
    private Transform _playerTransform;

    public NearEnemyFactory(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public Enemy CreateEnemy(string path, DataEnemy data)
    {
        var prefap = Resources.Load<GameObject>(path);
        var go = GameObject.Instantiate(prefap);
        var enemy = go.GetComponent<NearEnemy>();
        enemy.Initzialize(data, _playerTransform);
        return enemy;
    }
}

