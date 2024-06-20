using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BootLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<BootEntryPoint>();
    }
}
