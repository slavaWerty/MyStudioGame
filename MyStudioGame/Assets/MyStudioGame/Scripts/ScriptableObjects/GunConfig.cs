using UnityEngine;

[CreateAssetMenu(fileName = "GunConfig", menuName = "Game/new GunConfig")]
public class GunConfig : ScriptableObject
{
    [SerializeField] private DataGun _dataGun;

    public DataGun DataGun => _dataGun;
}
