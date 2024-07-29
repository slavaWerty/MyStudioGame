using DG.Tweening;
using UnityEngine;

public class FarAttackEnemyState : IEnemyState
{
    private Transform _transform;
    private Transform _playerTransform;
    private float _distance;
    private float _speed;
    private int _damage;
    private float _timeBetweenAttack;
    private float _startTimeBetweenAttack;
    private float _duration;
    private Transform _spawnBulletPosition;

    public FarAttackEnemyState(Transform transform, Transform playerTransform,
        float distance, float speed, int damage, float timeBetweenAttack, float duration, Transform spawnBulletPosition)
    {
        _transform = transform;
        _playerTransform = playerTransform;
        _distance = distance;
        _speed = speed;
        _damage = damage;
        _timeBetweenAttack = timeBetweenAttack;
        _startTimeBetweenAttack = _timeBetweenAttack;
        _duration = duration;
        _spawnBulletPosition = spawnBulletPosition;
    }

    public void Work()
    {
        var distance = Vector2.Distance(_playerTransform.position, _transform.position);

        if (distance > _distance)
        {
            var directionX = (_playerTransform.position - _transform.position).normalized.x;
            var direction = (new Vector3(directionX, 0)).normalized;

            _transform.position += direction * _speed * Time.deltaTime;
        }

        if (_timeBetweenAttack <= 0)
        {
            var direction = (_playerTransform.position - _transform.position).normalized;

            if(direction.x < 0)
            {
                _transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                _transform.rotation = Quaternion.Euler(0, 0, 0);
            }


            var prefap = Resources.Load<GameObject>("Prefaps/EnemyPatronTest");
            var go = GameObject.Instantiate(prefap, position: _spawnBulletPosition.position, Quaternion.identity);

            go.transform.position = _transform.position;
            go.GetComponent<PlayerDetection>().Initzialize(_damage);
            go.transform.DOMove(_playerTransform.position + new Vector3(_playerTransform.position.x, 0), _duration);
            GameObject.Destroy(go, _duration);

            _timeBetweenAttack = _startTimeBetweenAttack;
        }
        else
        {
            _timeBetweenAttack -= Time.deltaTime;
        }            
    }
}

