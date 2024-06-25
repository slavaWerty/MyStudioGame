using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IMovable, IDamagable
{
    public int Health { get; set; }

    public event Action Moved;
    public event Action TakeDamaged;

    public void Move(Vector3 direction)
    {
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
            gameObject.SetActive(false);
        }
    }
}
