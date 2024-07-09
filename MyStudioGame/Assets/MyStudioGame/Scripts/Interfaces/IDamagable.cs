using System;
using UnityEngine;

public interface IDamagable
{
    public int Health { get; set; }
    public void TakeDamage(int damage);
    public event Action TakeDamaged;

    public event Action<GameObject> Dead;
}
