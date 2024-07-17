using Infentory;
using Infentory.Contraller;
using Infentory.View;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class InfentoryLifetimeScope : LifetimeScope
{
    [SerializeField] private ScreenView _sreenView;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private ItemDetection _detection;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_size);
        builder.RegisterComponent(_detection);
        builder.RegisterComponent(_sreenView);

        var infentoryData = CreateData(_size);
        var infentory = new InfentoryGrid(infentoryData);

        builder.RegisterInstance(infentory);

        builder.Register<SelectedItem>(Lifetime.Singleton).WithParameter(infentory.InfentoriesSlot);
        builder.Register<UsingItem>(Lifetime.Singleton);

        builder.Register<ThrowItem>(Lifetime.Singleton);
        builder.Register<InfentoryController>(Lifetime.Singleton);

        builder.RegisterComponent(_detection.transform.GetChild(0));

        builder.Register<UsingItem>(Lifetime.Singleton).AsImplementedInterfaces();

        builder.RegisterEntryPoint<InfentoryEntryPoint>();
    }

    private InfentoryGridData CreateData(Vector2Int size)
    {
        var ceratedInfentorySlots = new List<InfentorySlotData>();
        var lenght = size.x * size.y;

        for (int i = 0; i < lenght; i++)
            ceratedInfentorySlots.Add(new InfentorySlotData());

        var createdInfentoryData = new InfentoryGridData
        {
            Size = size,
            Slots = ceratedInfentorySlots
        };

        return createdInfentoryData;
    }
}
