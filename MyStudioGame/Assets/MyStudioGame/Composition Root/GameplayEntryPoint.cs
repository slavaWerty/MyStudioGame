using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayEntryPoint : ITickable, IStartable
{
    [Inject] public AttackHandler _attackHandler;
    [Inject] public GunRotater _gunRotater;
    [Inject] public WearponSwitch _wearponSwitch;

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _attackHandler.IInput.OnAttack();
        }

        if(_attackHandler.Wearpon.GetType() == typeof(Gun))
        {
            // _gunRotater.Rotate();
        }

        if (Input.GetKey(KeyCode.E))
        {
            _wearponSwitch.IInput.OnSwitchWearpon();
        }            
    }

    public void Start()
    {
        Debug.Log("Work");
    }
}

