using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private int _damage;
    public Health Player { get; private set; }

    public void Initzialize(int damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null || collision.gameObject == null || !collision.gameObject.TryGetComponent(out Health health)) return;

        health.TakeDamage(_damage);
        Player = health;
        Destroy(gameObject);
    }
}

