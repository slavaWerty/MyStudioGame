using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameplayLifetimeScope : LifetimeScope
{
    private const string GunPath = "Prefaps/BaseGun";
    private const string SwordPath = "Prefaps/BaseSword";
    private const string CoroutinesName = "[Coroutines]";

    [Space(40)]
    [SerializeField] private GunConfig _gunConfig;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _containerBullet;
    [SerializeField] private SwordConfig _swordConfig;
    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private MovementConfig _movementconfig;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterCoroutines(builder);

        // RegisterEnemySystem(builder);

        RegisterInput(builder);

        RegisterMovementSystem(builder);

        RegisterWearponSystem(builder);

        builder.RegisterEntryPoint<GameplayEntryPoint>();
    }

    private void RegisterWearponSystem(IContainerBuilder builder)
    {
        builder.Register<Wearpons>(Lifetime.Singleton).WithParameter(_containerBullet);
        builder.Register<GunRotater>(Lifetime.Singleton);
        builder.Register<WearponSwitch>(Lifetime.Singleton);
        builder.Register<AttackHandler>(Lifetime.Singleton);
    }

    private void RegisterWearpons(IContainerBuilder builder, Transform spawnPosition)
    {
        builder.Register<IBulletFactory, BaseBulletFactory>(Lifetime.Singleton);

        var factory = new WearponFactory();

        var gun = factory.CreateGun(GunPath, _mainCamera);
        builder.RegisterInstance(_gunConfig);
        gun.gameObject.transform.position = spawnPosition.position;
        gun.transform.parent = spawnPosition;

        var sword = factory.CreateSword(SwordPath);
        builder.RegisterInstance(_swordConfig);
        sword.gameObject.transform.position = spawnPosition.position;
        sword.transform.parent = spawnPosition;

        builder.RegisterComponent(gun);
        builder.RegisterComponent(sword);
    }

    private static void RegisterInput(IContainerBuilder builder)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            builder.Register<IInput, MobileInput>(Lifetime.Singleton);
        else
            builder.Register<IInput, DekstopInput>(Lifetime.Singleton);
    }

    private void RegisterCoroutines(IContainerBuilder builder)
    {
        var coroutines = new GameObject(CoroutinesName);
        var prefap = coroutines.AddComponent<Coroutines>();

        builder.RegisterComponent(prefap);
    }

    private void RegisterEnemySystem(IContainerBuilder builder)
    {
        builder.Register<IEnemyFactory, BaseEnemyFactory>(Lifetime.Singleton);
        builder.RegisterInstance(_enemyConfig);
        builder.Register<EnemySpawner>(Lifetime.Singleton).WithParameter(_enemySpawnPoint);
    }

    private void RegisterMovementSystem(IContainerBuilder builder)
    {
        var factory = new BasePlayerFactory();
        var player = factory.Create(_playerSpawnPoint.position).GetComponent<Movement>();

        builder.RegisterComponent(player);

        builder.Register<MovementHandler>(Lifetime.Singleton).WithParameter(_movementconfig);

        RegisterWearpons(builder, player.transform.GetChild(0).transform);
    }
}
