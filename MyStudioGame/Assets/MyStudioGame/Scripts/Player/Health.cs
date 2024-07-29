using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;
    private int _currentHealth;

    public int HealthValue => _health;

    public event Action<float> TakeDamaged;

    private void Start()
    {
        _currentHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            TakeDamaged?.Invoke(0);
            Debug.Log("Died");
        }
        else
        {
            float currentHealthAsPersatenge = (float)_currentHealth / _health;
            TakeDamaged?.Invoke(currentHealthAsPersatenge);
        }
    }
}

