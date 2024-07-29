using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Health _health;

    private void OnTakeDamaged(float health)
    {
        _healthBar.fillAmount = health;
    }

    private void OnEnable()
    {
        _health.TakeDamaged += OnTakeDamaged;
    }

    private void OnDisable()
    {
        _health.TakeDamaged -= OnTakeDamaged;
    }
}

