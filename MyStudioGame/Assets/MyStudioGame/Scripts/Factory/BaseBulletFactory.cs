using UnityEngine;

public class BaseBulletFactory : IBulletFactory
{
    private const string Path = "Prefaps/BaseBullet";

    private Coroutines _coroutines;
    private Pool _pool;

    public BaseBulletFactory(Coroutines coroutines)
    {
        _coroutines = coroutines;
        _pool = new Pool();
    }

    public IBullet CreateBullet(Vector3 direction, Vector3 position, float duration, Transform container, float speed, int damage)
    {
        var prefap = Resources.Load<GameObject>(Path);
        var go = _pool.GetObject(prefap);
        go.transform.position = position;
        go.transform.parent = container;

        var bullet = go.AddComponent<BaseBullet>();
        bullet.Initzialize(speed, damage, direction, duration);

        _coroutines.StartCoroutine(_pool.PoolObjectWithTime(bullet.gameObject, duration, container));
        return bullet;
    }
}

