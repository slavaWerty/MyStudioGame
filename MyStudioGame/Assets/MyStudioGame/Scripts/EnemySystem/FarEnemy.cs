using System;
using UnityEngine;
using VContainer;

public class FarEnemy : Enemy, IDamagable
{
    private float _speed;

    private IEnemyState _attackState;
    private IEnemyState _idleState;

    public int Health { get; set; }

    public event Action TakeDamaged;

    [Inject]
    public override void Initzialize(EnemyConfig config, Transform playerTransform)
    {
        _speed = config.DataEnemy.Speed;
        Health = config.DataEnemy.Health;

        _attackState = new FarAttackEnemyState(transform, playerTransform, config.DataEnemy.distance,
            _speed, config.DataEnemy.Damage, config.DataEnemy.TimeBetweenAttack, config.DataEnemy.Duration, transform.GetChild(2));
        _idleState = new IdleEnemyState(transform, config.DataEnemy.Time);

        SetState(_idleState);
        _currentState.Work();

        Debug.Log("Init");
    }

    private void SetState(IEnemyState state)
    {
        _currentState = state;
    }

    private void Update()
    {
        _currentState.Work();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null || collision.gameObject == null) return;

        if (collision.gameObject.GetComponent<Movement>() == null) return;

        SetState(_attackState);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null || collision.gameObject == null) return;

        if (collision.gameObject.GetComponent<Movement>() == null) return;

        SetState(_idleState);
    }

    public override void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        Health -= damage;

        TakeDamaged?.Invoke();

        if (Health < 0)
        {
            Health = 0;
            Destroy(gameObject);
        }

        Debug.Log("Aaaaa");
    }
}
