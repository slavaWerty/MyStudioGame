using UnityEngine;

[CreateAssetMenu(fileName = "MovementConfig", menuName = "Game/new MovementConfig")]
public class MovementConfig : ScriptableObject
{
    [Header("Move")]
    [Space(10)]

    [SerializeField] private float _speed;

    public float Speed => _speed;

    [Header("Jump")]
    [Space(10)]

    [SerializeField] private float _jumpPower;
    [SerializeField] private LayerMask _layerMasl;

    public float JumpPower => _jumpPower;
    public LayerMask LayerMask => _layerMasl;
}

