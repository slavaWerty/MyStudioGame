using UnityEngine;
using DG.Tweening;

public class BaseBullet : MonoBehaviour, IBullet
{
    public float Speed { get; set; }
    public int Damage { get; set; }
    public Vector3 Direction { get; set; }
    public float Duration { get; set; }

    public void Initzialize(float speed, int damage, Vector3 direction, float duration)
    {
        Speed = speed;
        Damage = damage;
        Direction = direction;
        Duration = duration;
    }

    private void Start()
    {
        float angle = Vector2.SignedAngle(transform.right, Direction);
        transform.eulerAngles = new Vector3(0, 0, angle);

        transform.DOMove(Direction * Speed, Duration);
    }

    public void RestartDirection()
    {
        float angle = Vector2.SignedAngle(transform.right, Direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
}

