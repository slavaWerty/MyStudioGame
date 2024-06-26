using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Game/new EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private DataEnemy _dataEnemy;

    public DataEnemy DataEnemy => _dataEnemy;
}

