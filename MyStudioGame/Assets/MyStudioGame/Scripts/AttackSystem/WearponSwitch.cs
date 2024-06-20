using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class WearponSwitch : MonoBehaviour
{
    private IWearpon _currentWearpon;
    private List<IWearpon> _wearpons;
    private IInput _input;
    private int _index = 0;

    public IWearpon Wearpon => _currentWearpon;
    public IInput IInput => _input;

    [Inject]
    public void Construct(Wearpons wearpons, IInput input)
    {
        _wearpons = wearpons.Weapons;
        _currentWearpon = _wearpons[0];

        _input = input;
    }

    public void NextWearpon()
    {
        for (int i = 0; i < _wearpons.Count; i++)
        {
            if (_wearpons[i] == _currentWearpon)
            {
                _index = i;
                break;
            }
        }

        if (_index == _wearpons.Count - 1)
        {
            _currentWearpon = _wearpons[0];
            _index = 0;
        }
        else
        {
            _index++;
            _currentWearpon = _wearpons[_index];
        }
    }


    public void OnEnable()
    {
        _input.SwitchWearpon += NextWearpon;
    }

    public void OnDisable()
    {
        _input.SwitchWearpon -= NextWearpon;
    }
}

