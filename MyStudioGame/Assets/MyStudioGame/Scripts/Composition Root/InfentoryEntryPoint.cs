using UnityEngine;
using VContainer.Unity;
using VContainer;
using Infentory.Contraller;

public class InfentoryEntryPoint : ITickable, IStartable
{
    [Inject] private ItemDetection _detection;
    [Inject] private SelectedItem _selectedItem;
    [Inject] private ThrowItem _throwItem;
    [Inject] private InfentoryController _infentoryController;
    [Inject] private JsonSaver<GameStateData> _jsonSaver;
    [Inject] private GameStateData _gameStateData;
    [Inject] private UsingItem _usingItem;

    private string _ownerId;

    public InfentoryEntryPoint(string ownerId)
    {
        _ownerId = ownerId;
    }

    public void Start()
    {
        _infentoryController.OpenInventory(_ownerId);
        _selectedItem.OnSelectedItemChanged();
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_detection.CrystalInfentoryOwnerId == _ownerId)
                _detection.SearchItem();

            _selectedItem.OnSelectedItemChanged();
            _jsonSaver.Save(_gameStateData);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            _throwItem.Throw();
            _jsonSaver.Save(_gameStateData);
        }
    }
}

