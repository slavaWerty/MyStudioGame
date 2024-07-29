using Infentory;
using System;
using System.Collections.Generic;

public class SelectedItem : IDisposable
{
    private List<InfentorySlot> _infentoriesSlots;
    private List<SelectButton> _selectButtons;
    private InfentorySlot _currentSelectedSlot;

    public InfentorySlot CurrentSelectedSlot => _currentSelectedSlot;
    public List<InfentorySlot> InfentoriesSlots => _infentoriesSlots;

    public event Action SelectedObjectChanged;

    public SelectedItem(List<InfentorySlot> slots, List<SelectButton> selectButtons)
    {
        _infentoriesSlots = slots;
        _selectButtons = selectButtons;

        _currentSelectedSlot = _infentoriesSlots[0];
        _currentSelectedSlot.IsSelected = true;

        for (int i = 0; i < _selectButtons.Count; i++)
        {
            _selectButtons[i].OnChangeSlotViewSelected += NextSelected;
        }
    }

    public void NextSelected(int index)
    {
        _currentSelectedSlot.IsSelected = false;
        _currentSelectedSlot = _infentoriesSlots[index];
        _currentSelectedSlot.IsSelected = true;

        SelectedObjectChanged?.Invoke();
    }

    public InfentorySlot GetSelectedItem()
    {
        return _currentSelectedSlot;
    }

    public void OnSelectedItemChanged()
    {
        SelectedObjectChanged?.Invoke();
    }

    public void Dispose()
    {
        for (int i = 0; i < _selectButtons.Count; i++)
        {
            _selectButtons[i].OnChangeSlotViewSelected -= NextSelected;
        }
    }
}
