using DG.Tweening;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IMovable, IDamagable
{
    private float _duration;
    private float _lenght;

    public int Health { get; set; }

    public event Action Moved;
    public event Action TakeDamaged;
    public event Action<GameObject> Dead;

    public void Initzialize(DataEnemy data)
    {
        _duration = data.Duration;
        _lenght = data.Lenght;
        Health = data.Health;
    }

    public void Move(Vector3 direction)
    {
        transform.DOMove(direction * _lenght, _duration);

        Moved?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        TakeDamaged?.Invoke();

        Health -= damage;

        if(Health < 0)
        {
            Health = 0;
            Dead?.Invoke(gameObject);
            Debug.Log("Dead");
        }
    }
}
