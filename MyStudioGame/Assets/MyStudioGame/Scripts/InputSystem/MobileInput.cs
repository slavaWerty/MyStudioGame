using System;

internal class MobileInput : IInput
{
    public event Action Attack;
    public event Action SwitchWearpon;

    public void OnAttack()
    {
        Attack?.Invoke();
    }

    public void OnSwitchWearpon()
    {
        SwitchWearpon?.Invoke();
    }
}

