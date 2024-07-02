using System;

public interface IInput 
{
    public event Action Attack;
    public event Action SwitchWearpon;
    public event Action Move;
    public event Action Jump;

    public void OnAttack();
    public void OnSwitchWearpon();
    public void OnMove();
    public void OnJump();
}
