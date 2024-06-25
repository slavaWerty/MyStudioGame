using UnityEngine;

[CreateAssetMenu(fileName = "BaseBulletConfig", menuName = "Game/new BaseBulletConfig")]
public class BulletConfig : ScriptableObject
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _bulletDamage;

    public int BulletDamage => _bulletDamage;
    public float BulletSpeed => _bulletSpeed;
}
