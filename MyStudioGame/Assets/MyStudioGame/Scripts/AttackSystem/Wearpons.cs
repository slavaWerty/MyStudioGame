using System.Collections.Generic;
using UnityEngine;

public class Wearpons
{
    private List<IWearpon> _wearpons;
    private Transform _bulletContainer;

    public IBulletFactory _bulletFactory;

    public List<IWearpon> Weapons => _wearpons;

    public Wearpons(IBulletFactory bulletFactory, GunView gunView, 
        Transform bulletContainer, SwordView swordView, GunConfig gunConfig, SwordConfig swordConfig)
    {
        _bulletFactory = bulletFactory;
        _bulletContainer = bulletContainer;

        _wearpons = new List<IWearpon>
        {
            new Gun(_bulletFactory, gunView, _bulletContainer, gunConfig.DataGun),
            new Sword(swordView, swordConfig.DataSword)
        };
    }
}
