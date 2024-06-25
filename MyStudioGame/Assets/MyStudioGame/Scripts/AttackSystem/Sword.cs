using System.Collections.Generic;
using UnityEngine;

public class Sword : IWearpon
{
    private SwordView _swordView;
    private int _damage;
    private float _radius;

    public float StartTimeBtwShots { get; set; }

    public Sword(SwordView swordView, int damage, float radius)
    {
        _swordView = swordView;
        _damage = damage;
        _radius = radius;
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_swordView.AttackPoint.position, _radius);
        List<IDamagable> enemies = new List<IDamagable>();

        foreach (Collider2D item in colliders)
        {
            if(item.GetComponent<Enemy>() != null)
            {
                enemies.Add(item.GetComponent<Enemy>());
            }
        }

        foreach (IDamagable enemy in enemies)
        {
            enemy.TakeDamage(_damage);
        }
    }
}

