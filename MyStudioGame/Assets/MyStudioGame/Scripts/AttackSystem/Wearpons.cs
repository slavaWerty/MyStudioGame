using System.Collections.Generic;
using UnityEngine;

public class Wearpons
{
    private List<IWearpon> _wearpons;
    private float _gunTime = 2f;
    private Transform _bulletContainer;
    private int _swordDamage = 10;
    private float _swordRadius = 3f;


    public IBulletFactory _bulletFactory;

    public List<IWearpon> Weapons => _wearpons;

    public Wearpons(IBulletFactory bulletFactory, GunView gunView, Transform bulletContainer, SwordView swordView)
    {
        _bulletFactory = bulletFactory;
        _bulletContainer = bulletContainer;

        if (_bulletFactory == null)
            Debug.Log("Error");

        _wearpons = new List<IWearpon>
        {
            new Gun(_bulletFactory, _gunTime, gunView, _bulletContainer),
            new Sword(swordView, _swordDamage, _swordRadius)
        };
    }
}
