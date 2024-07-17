using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemConfig _itemData;
    [SerializeField] private int _amount;

    public ItemConfig ItemData => _itemData;
    public int Amount => _amount;

    public void Initzialize(int amount, ItemConfig config)
    {
        _amount = amount;
        _itemData = config;
    }
}

