using UnityEngine;

public class BaseEnemyFactory : IEnemyFactory
{
    public Enemy CreateEnemy(string path, DataEnemy data)
    {
        var prefap = Resources.Load<GameObject>(path);
        var go = GameObject.Instantiate(prefap);
        var enemy = go.AddComponent<Enemy>();
        enemy.Initzialize(data);
        return enemy;
    }
}

