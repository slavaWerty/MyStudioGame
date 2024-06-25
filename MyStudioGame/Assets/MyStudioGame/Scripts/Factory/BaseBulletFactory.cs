using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BaseBulletFactory : IBulletFactory
{
    private const string Path = "Prefaps/BaseBullet";

    private float _bulletSpeed;
    private int _bulletDamage;
    private Queue<GameObject> _bullets;
    private float _duration;
    private Coroutines _coroutines;

    public BaseBulletFactory(BulletConfig config, Coroutines coroutines)
    {
        _bulletSpeed = config.BulletSpeed;
        _bulletDamage = config.BulletDamage;

        _bullets = new Queue<GameObject>();
        _coroutines = coroutines;
    }

    public IBullet CreateBullet(Vector3 direction, Vector3 position, float duration, Transform container)
    {
        var prefap = Resources.Load<GameObject>(Path);
        var go = GetObject(prefap);
        go.transform.position = position;
        _duration = duration;
        go.transform.parent = container;

        var bullet = go.AddComponent<BaseBullet>();
        bullet.Initzialize(_bulletSpeed, _bulletDamage, direction, duration);

        _coroutines.StartCoroutine(PoolObject(bullet.gameObject, bullet));
        return bullet;
    }

    private IEnumerator PoolObject(GameObject bullet, BaseBullet baseBullet)
    {
        WaitForSeconds wait = new WaitForSeconds(_duration);

        yield return wait;

        _bullets.Enqueue(bullet);
        bullet.gameObject.SetActive(false);
        baseBullet.RestartDirection();
    }

    private GameObject GetObject(GameObject prefap)
    {
        if(_bullets.Count == 0)
        {
            var go = GameObject.Instantiate(prefap);
            return go;
        }

        _bullets.Peek().SetActive(true);
        return _bullets.Dequeue();
    }
}

