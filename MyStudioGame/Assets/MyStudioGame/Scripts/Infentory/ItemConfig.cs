using UnityEngine;

[CreateAssetMenu(fileName = "Itemconfig", menuName = "Game/new ItemConfig")]
public class ItemConfig : ScriptableObject
{
    [SerializeField] private int _amountCapacity;
    [SerializeField] private string _itemID;
    [SerializeField] private Sprite _sprite;


    public int AmountCapacity => _amountCapacity;
    public string ItemID => _itemID;
    public Sprite Sprite => _sprite;
}
