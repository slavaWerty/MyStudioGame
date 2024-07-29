using UnityEngine;

public class NearAttackEnemyState : IEnemyState
{
    private Transform _player;
    private Transform _transform;
    private float _speed;
    private float _attackRadius;
    private int _damage;
    private float _timeBetweenAttack;
    private float _startTimeBetweenAttack;

    public NearAttackEnemyState(Transform playerTransform, Transform transform,
        float speed, float attackRadius, int damage, float timeBetweenAttack)
    {
        _player = playerTransform;
        _transform = transform;
        _speed = speed;
        _attackRadius = attackRadius;
        _damage = damage;
        _timeBetweenAttack = timeBetweenAttack;
        _startTimeBetweenAttack = _timeBetweenAttack;
    }

    public void Work()
    {
        var directionX = (_player.position - _transform.position).normalized.x;
        var direction = new Vector3(directionX, 0);

        _transform.position += direction * Time.deltaTime * _speed;

        if (_timeBetweenAttack <= 0)
        {
            var colliders = Physics2D.OverlapCircleAll(_transform.position, _attackRadius);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Health health))
                {
                    health.TakeDamage(_damage);
                    break;
                }
            }

            _timeBetweenAttack = _startTimeBetweenAttack;
        }
        else
        {
            _timeBetweenAttack -= Time.deltaTime;
        }

    }
}

