using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class WearponSwitch : IDisposable
{
    private IWearpon _currentWearpon;
    private List<WearponItem> _wearpons;
    private IInput _input;
    private int _index = 0;
    private GameObject _currentView;
    private List<GameObject> _unselectedWeapons;

    public IWearpon Wearpon => _currentWearpon;
    public GameObject WearponView => _currentView;
    public IInput IInput => _input;

    [Inject]
    public void Construct(Wearpons wearpons, IInput input, GunView gunView, SwordView swordView)
    {
        _unselectedWeapons = new List<GameObject>();

        _wearpons = new List<WearponItem>{
            new WearponItem
            {
                Wearpon = wearpons.Weapons[0],
                WearponView = gunView.gameObject
            },
            new WearponItem
            {
                Wearpon = wearpons.Weapons[1],
                WearponView = swordView.gameObject
            }
        };

        for (int i = 0; i < _wearpons.Count; i++)
        {
            _unselectedWeapons.Add(_wearpons[i].WearponView);
            _wearpons[i].WearponView.SetActive(false);
        }

        _currentView = _unselectedWeapons[0];
        _currentView.SetActive(true);
        _unselectedWeapons.Remove(_unselectedWeapons[0]);

        _currentWearpon = _wearpons[0].Wearpon;

        _input = input;
        _input.SwitchWearpon += NextWearpon;
    }

    public void Dispose()
    {
        _input.SwitchWearpon -= NextWearpon;
    }

    public void NextWearpon()
    {
        for (int i = 0; i < _wearpons.Count; i++)
        {
            if (_wearpons[i].Wearpon == _currentWearpon)
            {
                _index = i;
                break;
            }
        }

        if (_index == _wearpons.Count - 1)
        {
            _currentWearpon = _wearpons[0].Wearpon;
            _index = 0;
        }
        else
        {
            _index++;
            _currentWearpon = _wearpons[_index].Wearpon;
        }

        ChangeView(_index);
    }

    private void ChangeView(int index)
    {
        for (int i = 0; i < _unselectedWeapons.Count; i++)
        {
            if(index > _unselectedWeapons.Count - 1)
                NewView(index - 1);

            if (i == index)
                NewView(index);
        }
    }

    private void NewView(int index)
    {
        _currentView.SetActive(false);
        _unselectedWeapons.Add(_currentView);

        _currentView = _unselectedWeapons[index];
        _currentView.SetActive(true);
        _unselectedWeapons.Remove(_currentView);
    }
}

