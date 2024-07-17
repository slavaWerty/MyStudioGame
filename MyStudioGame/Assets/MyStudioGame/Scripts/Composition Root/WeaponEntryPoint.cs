using UnityEngine;
using VContainer;
using VContainer.Unity;

public class WeaponEntryPoint : ITickable, IStartable
{
    private const float TimeBetweenWeaponChanges = 0.1f;

    [Inject] public WearponSwitch _wearponSwitch;
    [Inject] public AttackHandler _attackHandler;

    private float _startTimeBtwShots;
    private float _timeBetweenWeaponChanges;

    public void Start()
    {
   ///     _startTimeBtwShots = _wearponSwitch.Wearpon.StartTimeBtwShots;
        _timeBetweenWeaponChanges = TimeBetweenWeaponChanges;
    }

    public void Tick()
    {
  //      Attack();

        WearponSwitch();
    }

   // private void Attack()
   // {
     //   if (_wearponSwitch.Wearpon.StartTimeBtwShots <= 0)
     //   {
     //       if (Input.GetMouseButtonDown(0))
     ///       {
     //           _attackHandler.IInput.OnAttack();
       //         _wearponSwitch.Wearpon.StartTimeBtwShots = _startTimeBtwShots;
    //        }
    //    }
    //    else
    //    {
   //  //       _wearponSwitch.Wearpon.StartTimeBtwShots -= Time.deltaTime;
   //     }
  //  }

    private void WearponSwitch()
    {
        if (_timeBetweenWeaponChanges < 0)
        {
            if (Input.GetKey(KeyCode.E))
            {
                _wearponSwitch.IInput.OnSwitchWearpon();
                _timeBetweenWeaponChanges = TimeBetweenWeaponChanges;
       //         _startTimeBtwShots = _wearponSwitch.Wearpon.StartTimeBtwShots;
            }
        }
        else
        {
            _timeBetweenWeaponChanges -= Time.deltaTime;
        }
    }
}

