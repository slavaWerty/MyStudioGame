using ScriptableObjects;
using UnityEngine;

public class Wearpons
{
   // private List<IItemLogic> _wearpons;
    private Transform _bulletContainer;

    public IBulletFactory _bulletFactory;

   // public List<IItemLogic> Weapons => _wearpons;

    public Wearpons(IBulletFactory bulletFactory, GunView gunView, 
        Transform bulletContainer, SwordView swordView, GunConfig gunConfig, SwordConfig swordConfig)
    {
        _bulletFactory = bulletFactory;
        _bulletContainer = bulletContainer;

      //  _wearpons = new List<IItemLogic>
       /// {
       ////     new Gun(_bulletFactory, gunView, _bulletContainer, gunConfig.DataGun),
       //     new Sword(swordView, swordConfig.DataSword)
       // };
    }
}
