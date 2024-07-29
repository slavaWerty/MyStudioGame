using Infentory;
using UnityEngine;

public class ThrowItem
{
    private SelectedItem _selectedItem;
    private InfentoryGrid _infentory;
    private Transform _spawnPosition;
    private UsingItem _usingItem;

    private InfentorySlot Slot => _selectedItem.CurrentSelectedSlot;

    public ThrowItem(SelectedItem selectedItem, InfentoryGrid infentory, Transform spawnPosition, UsingItem usingItem)
    {
        _selectedItem = selectedItem;
        _infentory = infentory;
        _spawnPosition = spawnPosition;
        _usingItem = usingItem;
    }

    public void Throw()
    {
        if (Slot.isEmpty)
        {
            Debug.Log("Null");
            return;
        }

        if (CreateItem() == null)
            return;

        _infentory.RemoveItems(_infentory.GetCoordinate(Slot), Slot.ItemID, Slot.Amount);
        GameObject.Destroy(_usingItem.CurrentObject);
    }

    private GameObject CreateItem()
    {
        GameObject go;

        var prefap = Resources.Load<GameObject>($"Prefaps/Throw{Slot.ItemID}");

        if (prefap != null)
            go = GameObject.Instantiate(prefap);
        else
        {
            Debug.Log("Item not Prefap Object");
            return null;
        }

        go.transform.position = _spawnPosition.position;

        _selectedItem.OnSelectedItemChanged();

        return go;
    }
}

