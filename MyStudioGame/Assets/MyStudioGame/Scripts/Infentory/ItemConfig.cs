using UnityEngine;

[CreateAssetMenu(fileName = "Itemconfig", menuName = "Game/new ItemConfig")]
public class ItemConfig : ScriptableObject
{
    [SerializeField] private int _amountCapacity;
    [SerializeField] private string _itemID;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private bool _isCrystal;
    [SerializeField] private int _damageBuff;
    [SerializeField] private int _healthBuff;

    public int AmountCapacity => _amountCapacity;
    public string ItemID => _itemID;
    public Sprite Sprite => _sprite;
    public bool IsCrystal => _isCrystal;
    public int HealthBuff => _healthBuff;
    public int DamageBuff => _damageBuff;
}
