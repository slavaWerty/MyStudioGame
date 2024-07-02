using System;

public class DekstopInput : IInput
{
    public event Action Attack;
    public event Action SwitchWearpon;
    public event Action Move;
    public event Action Jump;

    public void OnAttack()
    {
        Attack?.Invoke();
    }

    public void OnJump()
    {
        Jump?.Invoke();
    }

    public void OnMove()
    {
        Move?.Invoke();
    }

    public void OnSwitchWearpon()
    {
        SwitchWearpon?.Invoke();
    }
}

