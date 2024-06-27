using UnityEngine;

[CreateAssetMenu(fileName = "SwordConfig", menuName = "Game/new SwordConfig")]
public class SwordConfig : ScriptableObject
{
    [SerializeField] private DataSword _dataSword;

    public DataSword DataSword => _dataSword;
}

