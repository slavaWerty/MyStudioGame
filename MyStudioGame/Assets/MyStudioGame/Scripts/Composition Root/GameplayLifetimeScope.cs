using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameplayLifetimeScope : LifetimeScope
{
    [Space(40)]
    [SerializeField] private BulletConfig _bulletConfig;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _containerBullet;

    protected override void Configure(IContainerBuilder builder)
    {
        var coroutines = new GameObject("[Coroutines]");
        var prefap = coroutines.AddComponent<Coroutines>();

        builder.RegisterComponent(prefap);

        if (SystemInfo.deviceType == DeviceType.Handheld)
            builder.Register<IInput, MobileInput>(Lifetime.Singleton);
        else
            builder.Register<IInput, DekstopInput>(Lifetime.Singleton);

        builder.Register<IBulletFactory, BaseBulletFactory>(Lifetime.Singleton).WithParameter(_bulletConfig);

        var factory = new WearponFactory();

        var gun = factory.CreateGun("Prefaps/BaseGun", _mainCamera);
        var sword = factory.CreateSword("Prefaps/BaseSword");

        builder.RegisterComponent(gun);
        builder.RegisterComponent(sword);

        builder.Register<Wearpons>(Lifetime.Singleton).WithParameter(_containerBullet);
        builder.Register<GunRotater>(Lifetime.Singleton);
        builder.Register<WearponSwitch>(Lifetime.Singleton);
        builder.Register<AttackHandler>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameplayEntryPoint>();
    }
}
