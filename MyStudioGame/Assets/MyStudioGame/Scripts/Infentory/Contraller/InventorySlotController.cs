using Infentory.ReadOnly;
using Infentory.View;
using UnityEngine;

namespace Infentory.Contraller
{
    public class InventorySlotController
    {
        private InfentorySlotView _view;

        public InventorySlotController(IReadOnlyInfentorySlot slot, InfentorySlotView view)
        {
            _view = view;

            slot.ItemIDChanfed += SlotOnItemIDChanged;
            slot.ItemAmountchanged += OnSlotItemAmountChanged;
            slot.SpriteChanged += OnSpriteChanged;
            slot.ChangeSelected += OnChangeSelected;

            view.TextTitle = slot.ItemID;
            view.Amount = slot.Amount;
            view.Sprite = slot.Sprite;
            view.IsSelected = slot.IsSelected;
        }

        private void OnChangeSelected(bool obj)
        {
            _view.IsSelected = obj;
        }

        private void OnSpriteChanged(Sprite obj)
        {
            _view.Sprite = obj;
        }

        private void OnSlotItemAmountChanged(int obj)
        {
            _view.Amount = obj;
        }

        private void SlotOnItemIDChanged(string obj)
        {
            _view.TextTitle = obj;
        }
    }
}
