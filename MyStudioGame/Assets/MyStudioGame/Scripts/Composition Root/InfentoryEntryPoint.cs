using UnityEngine;
using VContainer.Unity;
using VContainer;
using Infentory.Contraller;
using Infentory;
using Infentory.View;

public class InfentoryEntryPoint : ITickable, IStartable
{
    private const float TimeBetweenThrowItem = 0.1f;

    [Inject] private ItemDetection _detection;
    [Inject] private Vector2Int _size;
    [Inject] private ScreenView _view;
    [Inject] private SelectedItem _selectedItem;
    [Inject] private ThrowItem _throwItem;
    [Inject] private InfentoryGrid _infentory;
    [Inject] private InfentoryController _infentoryController;
    [Inject] private UsingItem _usingItem;


    private float _timeBetweenThrowItem;

    public void Start()
    {
        _infentoryController.OpenInventory();
        _timeBetweenThrowItem = TimeBetweenThrowItem;
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var item = _detection.SearchItem();

            if (item == null)
                return;

            _infentory.AddItemConfig(item.ItemData);
            _infentory.AddItems(item.ItemData.ItemID, item.ItemData.Sprite, item.Amount);
            _selectedItem.OnSelectedItemChanged();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            _selectedItem.NextSelected();
        }

        if (_timeBetweenThrowItem <= 0)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                _throwItem.Throw();
                _timeBetweenThrowItem = TimeBetweenThrowItem;
            }
        }
        else
        {
            _timeBetweenThrowItem -= Time.deltaTime;
        }
    }
}

