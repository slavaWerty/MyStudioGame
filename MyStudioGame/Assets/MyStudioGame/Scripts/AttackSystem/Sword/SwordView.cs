using ScriptableObjects;
using UnityEngine;

public class SwordView : MonoBehaviour
{
    [SerializeField] private SwordConfig _config;

    private Transform _attackPoint;
    private Sword _sword;

    public Transform AttackPoint => _attackPoint;

    public void Start()
    {
        _attackPoint = transform.GetChild(0);
        _sword = new Sword(this, _config.DataSword);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _sword.Attack();
            Debug.Log("Attack");
        }
    }
}

