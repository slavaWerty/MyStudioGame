using UnityEngine;

public class WearponSwitch : MonoBehaviour
{
    public IWearpon _wearpon;
    public GunRotater _rotater;

    private void Start()
    {
        SetWearpon(new Gun());
        _wearpon.Attack();
        _rotater = new GunRotater();
    }

    public void SetWearpon(IWearpon wearpon)
    {
        _wearpon = wearpon;

        if (_wearpon == new Gun())
        {
            _rotater.Rotate();
        }
    }
}

