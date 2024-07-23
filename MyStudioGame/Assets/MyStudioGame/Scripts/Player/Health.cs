using System;

public class Health : IDisposable
{
    private int _health;
    private Buffs _buffs;
    private int _startHealth;

    public int HealthValue => _health;

    public event Action Died;

    public Health(Buffs buffs, int health)
    {
        _health = health;
        _buffs = buffs;
        _startHealth = health;

        _buffs.PlayerBuffsChanged += OnPlayerBuffsChanged;
    }

    private void OnPlayerBuffsChanged()
    {
        _health += _startHealth + _buffs.PlayerBuffs.HealthBuff;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Died?.Invoke();
        }
    }

    public void Dispose()
    {
        _buffs.PlayerBuffsChanged -= OnPlayerBuffsChanged;
    }
}

