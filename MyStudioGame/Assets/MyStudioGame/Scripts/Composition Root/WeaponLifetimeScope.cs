using VContainer;
using VContainer.Unity;
using UnityEngine;
using ScriptableObjects;

public class WeaponLifetimeScope : LifetimeScope
{
    private const string GunPath = "Prefaps/BaseGun";
    private const string SwordPath = "Prefaps/BaseSword";

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GunConfig _gunConfig;
    [SerializeField] private SwordConfig _swordConfig;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _containerBullet;
    [SerializeField] private Coroutines _corotines;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_corotines);

        RegisterInput(builder);

        RegisterWearpons(builder, _spawnPoint);

        RegisterWearponSystem(builder);

        builder.RegisterEntryPoint<WeaponEntryPoint>();
    }


    private void RegisterWearpons(IContainerBuilder builder, Transform spawnPosition)
    {
        builder.Register<IBulletFactory, BaseBulletFactory>(Lifetime.Singleton);

        var factory = new WearponFactory();

        var gun = factory.CreateGun(GunPath, _mainCamera);
        builder.RegisterComponent(_gunConfig);
        gun.gameObject.transform.position = spawnPosition.position;
        gun.transform.parent = spawnPosition;

        var sword = factory.CreateSword(SwordPath);
        builder.RegisterComponent(_swordConfig);
        sword.gameObject.transform.position = spawnPosition.position;
        sword.transform.parent = spawnPosition;

        builder.RegisterComponent(gun);
        builder.RegisterComponent(sword);
    }

    private void RegisterWearponSystem(IContainerBuilder builder)
    {
        builder.Register<Wearpons>(Lifetime.Singleton).WithParameter(_containerBullet);
        builder.Register<WearponSwitch>(Lifetime.Singleton);
        builder.Register<AttackHandler>(Lifetime.Singleton);
    }

    private static void RegisterInput(IContainerBuilder builder)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            builder.Register<IInput, MobileInput>(Lifetime.Singleton);
        else
            builder.Register<IInput, DekstopInput>(Lifetime.Singleton);
    }
}
