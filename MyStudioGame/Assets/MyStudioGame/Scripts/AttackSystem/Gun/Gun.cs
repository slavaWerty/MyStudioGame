using Infentory;
using System;
using System.Collections;
using UnityEngine;

public class Gun
{
    private const string Name = "DataGun";
    private const string GunInitKey = "GunInitKey";

    private IBulletFactory _bulletFactory;
    private GunInitzializer _view;
    private Transform _container;
    private DataGun _dataGun;
    private int _ammo;
    private int _currentAmmo;
    private float _timeBetweenReload;
    private int _allAmmo;
    private bool _isReloaded;
    private InfentoryService _service;

    public float StartTimeBtwShots { get; set; }

    public event Action<int, int> Reloaded;

    public Gun(IBulletFactory bulletFactory, GunInitzializer gunView,
        Transform container, DataGun config, InfentoryService service)
    {
        LoadData(config);

        _bulletFactory = bulletFactory;
        StartTimeBtwShots = _dataGun.StartTimeBtwShots;
        _view = gunView;
        _container = container;
        _ammo = config.Ammo;
        _timeBetweenReload = config.TimeBetweenReload;
        _allAmmo = config.AllAmmo;
        _service = service;

        gunView.StartCoroutine(Reload(config.TimeBetweenReload));
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
        if (_currentAmmo > 0)
        {
            if (!_isReloaded)
            {
                _bulletFactory.CreateBullet(_view.Direction,
                    _view.SpawnPosition.position, _dataGun.Duration, _container, _dataGun.BulletSpeed, _dataGun.BulletDamage);
                _currentAmmo--;
            }
        }
        else
        {
            if (!_isReloaded)
            {
                _view.StartCoroutine(Reload(_timeBetweenReload));
            }
        }

        OnReloaded();
    }

    public IEnumerator Reload(float time)
    {
        _isReloaded = true;

        var wait = new WaitForSeconds(time);

        yield return wait;

        if (_service.GetInventory("Base").GetAmount("Patron") > 0)
        {
            var allAmmo = _service.GetInventory("Base").GetAmount("Patron");
            int reason = _ammo - _currentAmmo;

            if (allAmmo >= reason)
            {
                _service.RemoveItemToInfentory("Base", "Patron", reason);
                _currentAmmo = _ammo;
            }
            else
            {
                _currentAmmo += allAmmo;
                _service.RemoveItemToInfentory("Base", "Patron", _service.GetInventory("Base").GetAmount("Patron"));
            }
        }


        OnReloaded();
        _isReloaded = false;
    }

    public void OnReloaded()
    {
        Reloaded?.Invoke(_currentAmmo, _allAmmo);
    }
}

