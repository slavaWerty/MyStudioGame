using UnityEngine;

public class Gun : IWearpon
{
    private IBulletFactory _bulletFactory;
    private GunView _view;
    private float _duration = 5f;
    private Transform _container;

    public float StartTimeBtwShots { get; set; }

    public Gun(IBulletFactory bulletFactory, float startTimeBtwShots, GunView gunView, Transform container)
    {
        _bulletFactory = bulletFactory;
        StartTimeBtwShots = startTimeBtwShots;
        _view = gunView;
        _container = container;
    }


    public void Attack()
    {
         _bulletFactory.CreateBullet(_view.Direction, _view.transform.position, _duration, _container);
    }
}

