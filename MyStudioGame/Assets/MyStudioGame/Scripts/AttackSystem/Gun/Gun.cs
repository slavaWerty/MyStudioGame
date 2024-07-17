using UnityEngine;

public class Gun
{
    private const string Name = "DataGun";
    private const string GunInitKey = "GunInitKey";

    private IBulletFactory _bulletFactory;
    private GunView _view;
    private Transform _container;
    private DataGun _dataGun;

    public float StartTimeBtwShots { get; set; }

    public Gun(IBulletFactory bulletFactory, GunView gunView, Transform container, DataGun config)
    {
        LoadData(config);

        _bulletFactory = bulletFactory;
        StartTimeBtwShots = _dataGun.StartTimeBtwShots;
        _view = gunView;
        _container = container;
    }

    private void LoadData(DataGun dataGun)
    {
        var saver = new JsonSaver<DataGun>(Name);

        if (PlayerPrefs.GetInt(GunInitKey) == 0)
        {
            _dataGun = dataGun;
            saver.Save(_dataGun);

            PlayerPrefs.SetInt(GunInitKey, 1);
        }
        else
        {
            _dataGun = saver.Load();
        }
    }

    public void Attack()
    {
        _bulletFactory.CreateBullet(_view.Direction,
            _view.transform.position, _dataGun.Duration, _container, _dataGun.BulletSpeed, _dataGun.BulletDamage);
    }

    public void UsingLogic()
    {
        Attack();
        Debug.Log("Using");
    }
}

