using UnityEngine;

public class NearAttackEnemyState : IEnemyState
{
    private Transform _player;
    private Transform _transform;
    private float _speed;

    public NearAttackEnemyState(Transform playerTransform, Transform transform, float speed)
    {
        _player = playerTransform;
        _transform = transform;
        _speed = speed;
    }

    public void Work()
    {
        var directionX = (_player.position - _transform.position).normalized.x;
        var direction = new Vector3(directionX, 0);

        _transform.position += direction * Time.deltaTime * _speed;
    }
}

