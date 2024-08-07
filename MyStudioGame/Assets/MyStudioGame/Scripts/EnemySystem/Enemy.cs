﻿using UnityEngine;
using VContainer;

public abstract class Enemy : MonoBehaviour
{
    protected IEnemyState _currentState;

    [Inject]
    public virtual void Initzialize() { }

    [Inject]
    public virtual void Initzialize(EnemyConfig data, Transform playerTransform) { }

    [Inject]
    public virtual void Initzialize(DataEnemy data, Transform playerTransform) { }

    public abstract void TakeDamage(int damage);
}

