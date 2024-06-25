using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayEntryPoint : ITickable, IStartable
{
    private const float TimeBetweenWeaponChanges = 0.1f;

    [Inject] public AttackHandler _attackHandler;
    [Inject] public GunRotater _gunRotater;
    [Inject] public WearponSwitch _wearponSwitch;

    private float _timeBetweenWeaponChanges;

    public void Start()
    {
        _timeBetweenWeaponChanges = TimeBetweenWeaponChanges;
    }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _attackHandler.IInput.OnAttack();
        }

        if (_attackHandler.Wearpon.GetType() == typeof(Gun))
        {
            _gunRotater.Rotate();
        }

        if (_timeBetweenWeaponChanges < 0)
        {
            if (Input.GetKey(KeyCode.E))
            {
                _wearponSwitch.IInput.OnSwitchWearpon();
                _timeBetweenWeaponChanges = TimeBetweenWeaponChanges;
            }
        }
        else
        {
            _timeBetweenWeaponChanges -= Time.deltaTime;
        }
    }
}

