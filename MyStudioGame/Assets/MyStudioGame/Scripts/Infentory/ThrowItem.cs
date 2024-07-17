using Infentory;
using UnityEngine;

public class ThrowItem
{
    private SelectedItem _selectedItem;
    private InfentoryGrid _infentory;

    private InfentorySlot Slot => _selectedItem.CurrentSelectedSlot;

    public ThrowItem(SelectedItem selectedItem, InfentoryGrid infentory)
    {
        _selectedItem = selectedItem;
        _infentory = infentory;
    }

    public void Throw()
    {
        if (Slot.isEmpty)
        {
            Debug.Log("Null");
            return;
        }

        CreateItem();

        _infentory.RemoveItems(_infentory.GetCoordinate(Slot), Slot.ItemID, Slot.Amount);
    }

    private void CreateItem()
    {
        var prefap = Resources.Load<GameObject>("Prefaps/Square");
        var go = GameObject.Instantiate(prefap) as GameObject;
        go.name = Slot.ItemID;

        go.AddComponent<BoxCollider2D>();
        go.AddComponent<Rigidbody2D>();
        var spriteRenderer = go.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Slot.Sprite;
        var item = go.AddComponent<Item>();
        var config = _infentory.GetItemConfig(Slot.ItemID);
        item.Initzialize(Slot.Amount, config);
        Debug.Log("ThrowItem");
    }
}

