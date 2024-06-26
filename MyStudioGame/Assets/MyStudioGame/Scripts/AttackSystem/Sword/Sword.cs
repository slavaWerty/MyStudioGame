using System.Collections.Generic;
using UnityEngine;

public class Sword : IWearpon
{
    private const string Name = "DataSword";
    private const string KeyInitSword = "KeyInitSword";

    private SwordView _swordView;
    private DataSword _dataSword;

    public float StartTimeBtwShots { get; set; }

    public Sword(SwordView swordView, DataSword data)
    {
        LoadData(data);

        _swordView = swordView;
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_swordView.AttackPoint.position, _dataSword.Radius);
        List<IDamagable> enemies = new List<IDamagable>();

        foreach (Collider2D item in colliders)
        {
            if (item.GetComponent<Enemy>() != null)
            {
                enemies.Add(item.GetComponent<Enemy>());
            }
        }

        foreach (IDamagable enemy in enemies)
        {
            enemy.TakeDamage(_dataSword.Damage);
        }
    }

    private void LoadData(DataSword dataSword)
    {
        var saver = new JsonSaver<DataSword>(Name);

        if (PlayerPrefs.GetInt(KeyInitSword) == 0)
        {
            _dataSword = dataSword;
            saver.Save(_dataSword);

            Debug.Log("Save");

            PlayerPrefs.SetInt(KeyInitSword, 1);
        }
        else
        {
            Debug.Log("Load");

            _dataSword = saver.Load();
        }
    }
}

