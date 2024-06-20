using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private WearponSwitch _switcher;

    protected override void Configure(IContainerBuilder builder)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            builder.Register<IInput, MobileInput>(Lifetime.Singleton);
        else
            builder.Register<IInput, DekstopInput>(Lifetime.Singleton);

        builder.Register<Wearpons>(Lifetime.Singleton);
        builder.Register<GunRotater>(Lifetime.Singleton);

        builder.RegisterComponent(_switcher);

        builder.Register<AttackHandler>(Lifetime.Singleton);

        builder.RegisterEntryPoint<GameplayEntryPoint>();
    }
}
