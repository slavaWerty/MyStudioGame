using System;
using UnityEngine;
using VContainer;

public class NearEnemy : Enemy, IDamagable
{
    private float _speed;

    private IEnemyState _attackState;
    private IEnemyState _idleState;

    public int Health { get; set; }

    public event Action TakeDamaged;
    public event Action<GameObject> Dead;

    [Inject]
    public override void Initzialize(EnemyConfig config, Movement playerTransform)
    {
        _speed = config.DataEnemy.Speed;
        Health = config.DataEnemy.Health;

        _attackState = new NearAttackEnemyState(playerTransform.transform, transform, _speed);
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

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        TakeDamaged?.Invoke();

        Health -= damage;

        if (Health < 0)
        {
            Health = 0;
            Dead?.Invoke(gameObject);
        }
    }
}

