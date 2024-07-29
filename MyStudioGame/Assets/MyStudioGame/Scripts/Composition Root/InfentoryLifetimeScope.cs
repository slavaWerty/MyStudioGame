using Infentory;
using Infentory.Contraller;
using Infentory.View;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class InfentoryLifetimeScope : LifetimeScope
{
    private string GameStateDataName => $"{_ownerId}GameStateDataName";
    private string GameStateDataInitKey => $"{_ownerId}GameStateDataInitKey";

    [SerializeField] private ScreenView _sreenView;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private ItemDetection _detection;
    [SerializeField] private InfentoryService _infentoryService;
    [SerializeField] private Coroutines _coroutines;
    [SerializeField] private Camera _maincamera;
    [Space(20)]
    [SerializeField] private string _ownerId;
    [SerializeField] private bool _isCrystalInfentory;

    [field: SerializeField] private List<SelectButton> _selectButtons = new();

    private GameStateData _gameStateData;
    private JsonSaver<GameStateData> _jsonSaver;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_size);
        builder.RegisterComponent(_sreenView);
        builder.RegisterComponent(_coroutines);
        builder.RegisterComponent(_maincamera);

        _infentoryService.RegisterInfentory(InitzializeData(_size), _isCrystalInfentory);

        var infentory = _infentoryService.GetInventory(_ownerId);

        Debug.Log(_ownerId);

        builder.RegisterInstance(infentory);
        builder.RegisterComponent(_selectButtons);

        builder.Register<SelectedItem>(Lifetime.Singleton).WithParameter(infentory.InfentoriesSlot);

        builder.Register<UsingItem>(Lifetime.Singleton).WithParameter(_isCrystalInfentory);

        builder.Register<ThrowItem>(Lifetime.Singleton).WithParameter(_detection.transform);

        builder.RegisterComponent(_infentoryService);
        builder.RegisterComponent(_detection);
        builder.Register<InfentoryController>(Lifetime.Singleton).WithParameter(_infentoryService);

        builder.RegisterComponent(_detection.transform.GetChild(0));

        builder.Register<UsingItem>(Lifetime.Singleton).AsImplementedInterfaces();

        builder.RegisterInstance(_jsonSaver);
        builder.RegisterInstance(_gameStateData);

        builder.RegisterEntryPoint<InfentoryEntryPoint>().WithParameter(_ownerId);
    }

    private InfentoryGridData InitzializeData(Vector2Int size)
    {
        var ceratedInfentorySlots = new List<InfentorySlotData>();
        var lenght = size.x * size.y;

        for (int i = 0; i < lenght; i++)
            ceratedInfentorySlots.Add(new InfentorySlotData());

        var createdInfentoryData = new InfentoryGridData
        {
            Size = size,
            Slots = ceratedInfentorySlots,
            OwnerID = _ownerId
        };

        LoadData(new GameStateData
        {
            InfentoryData = createdInfentoryData
        });

        return createdInfentoryData;
    }

    private void LoadData(GameStateData startData)
    {
        _jsonSaver = new JsonSaver<GameStateData>(GameStateDataName);

        if (PlayerPrefs.GetInt(GameStateDataInitKey) == 0)
        {
            _gameStateData = startData;
            _jsonSaver.Save(_gameStateData);

            PlayerPrefs.SetInt(GameStateDataInitKey, 1);
        }
        else
        {
            _gameStateData = _jsonSaver.Load();
        }
    }
}