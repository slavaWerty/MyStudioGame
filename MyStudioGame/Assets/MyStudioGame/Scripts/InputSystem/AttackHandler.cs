using System;

public class AttackHandler // : IDisposable
{
    private IInput _iInput;
    private WearponSwitch _wearponSwitch;

   // public IItemLogic Wearpon => _wearponSwitch.Wearpon;

    public IInput IInput => _iInput;

    public AttackHandler(IInput input, WearponSwitch wearponSwitch)
    {
        _iInput = input;
        _wearponSwitch = wearponSwitch;

   //     IInput.Attack += OnClickDown;
    }

  //  public void Dispose()
  // {
   //     IInput.Attack -= OnClickDown;
   // }

  //  private void OnClickDown()
  ///  {
    //    Wearpon.Attack();
    //}
}

