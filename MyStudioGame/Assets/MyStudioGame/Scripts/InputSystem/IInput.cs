using System;

public interface IInput 
{
    public event Action Attack;
    public event Action SwitchWearpon;

    public void OnAttack();
    public void OnSwitchWearpon();
}
