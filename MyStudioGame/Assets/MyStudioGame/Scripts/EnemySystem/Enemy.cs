using UnityEngine;
using VContainer;

public abstract class Enemy : MonoBehaviour
{
    protected IEnemyState _currentState;

    [Inject]
    public virtual void Initzialize() { }

    [Inject]
    public virtual void Initzialize(EnemyConfig data, Movement playerTransform) { }

    [Inject]
    public virtual void Initzialize(DataEnemy data, Transform playerTransform) { }
}

