using VContainer;
using VContainer.Unity;
using UnityEngine;

public class PlayerLifetimeScope : LifetimeScope
{
    [Space(40)]
    [SerializeField] private MovementConfig _movementconfig;
    [SerializeField] private MovementHandler _movementHandler;
    [SerializeField] private Movement _movement;
    [SerializeField] private Camera _camera;
    

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterInput(builder);

        builder.RegisterInstance(_movementconfig);
        builder.RegisterComponent(_movement);
        builder.RegisterComponent(_camera);

        builder.Register<MovementHandler>(Lifetime.Singleton);
        builder.Register<RotatePlayer>(Lifetime.Singleton).WithParameter(_movement.transform);

        builder.RegisterEntryPoint<PlayerEntryPoint>();
    }

    private static void RegisterInput(IContainerBuilder builder)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            builder.Register<IInput, MobileInput>(Lifetime.Singleton);
        else
            builder.Register<IInput, DekstopInput>(Lifetime.Singleton);
    }
}
