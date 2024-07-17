using Infentory;
using System;
using System.Collections.Generic;

public class SelectedItem 
{
    private List<InfentorySlot> _infentoriesSlots;
    private InfentorySlot _currentSelectedSlot;
    private int _index = 0;

    public InfentorySlot CurrentSelectedSlot => _currentSelectedSlot;

    public event Action SelectedObjectChanged;

    public SelectedItem(List<InfentorySlot> slots)
    {
        _infentoriesSlots = slots;

        _currentSelectedSlot = _infentoriesSlots[0];
        _currentSelectedSlot.IsSelected = true;
    }

    public InfentorySlot NextSelected()
    {
        if(_index >= _infentoriesSlots.Count - 1)
        {
            _index = 0;
        }
        else if(_index < _infentoriesSlots.Count - 1)
        {
            _index++;
        }

        _currentSelectedSlot.IsSelected = false;
        _currentSelectedSlot = _infentoriesSlots[_index];
        _currentSelectedSlot.IsSelected = true;
        SelectedObjectChanged?.Invoke();

        return _currentSelectedSlot;
    }

    public void OnSelectedItemChanged()
    {
        SelectedObjectChanged?.Invoke();
    }
}
