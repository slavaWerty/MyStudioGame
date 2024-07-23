using ScriptableObjects;
using UnityEngine;

public class SwordView : MonoBehaviour
{
    [SerializeField] private SwordConfig _config;
    [SerializeField] private float _timeBetweenAttack;

    private Transform _attackPoint;
    private Sword _sword;
    private float _tempTimeBetweenAttack;

    public Transform AttackPoint => _attackPoint;

    public void Start()
    {
        _tempTimeBetweenAttack = _timeBetweenAttack;

        _attackPoint = transform.GetChild(0);
        _sword = new Sword(this, _config.DataSword);
    }

    private void Update()
    {
        if (_timeBetweenAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _sword.Attack();
                _timeBetweenAttack = _tempTimeBetweenAttack;
            }
        }
        else
        {
            _timeBetweenAttack -= Time.deltaTime;
        }
    }
}

