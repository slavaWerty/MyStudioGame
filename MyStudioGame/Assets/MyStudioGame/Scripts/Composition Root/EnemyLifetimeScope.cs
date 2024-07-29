using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyLifetimeScope : LifetimeScope
{
    [SerializeField] private EnemyInjector _injector;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_injector);
    }
}
