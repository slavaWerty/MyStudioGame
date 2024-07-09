using UnityEngine;

public class FarAttackEnemyState : IEnemyState
{
    private Transform _transform;
    private Transform _playerTransform;
    private float _distance;
    private float _speed;

    public FarAttackEnemyState(Transform transform, Transform playerTransform, float distance, float speed)
    {
        _transform = transform;
        _playerTransform = playerTransform;
        _distance = distance;
        _speed = speed;
    }

    public void Work()
    {
        var distance = Vector2.Distance(_playerTransform.position, _transform.position);

        if(distance > _distance)
        {
            var directionX = (_playerTransform.position - _transform.position).normalized.x;
            var direction = (new Vector3(directionX, 0)).normalized;

            _transform.position += direction * _speed * Time.deltaTime;
        }
    }
}

