using Infentory.ReadOnly;
using System;
using UnityEngine;

namespace Infentory
{
    public class InfentorySlot : IReadOnlyInfentorySlot
    {
        public bool isEmpty => Amount == 0 && string.IsNullOrEmpty(ItemID);

        private bool Selected;

        public event Action<string> ItemIDChanfed;
        public event Action<int> ItemAmountchanged;
        public event Action<Sprite> SpriteChanged;
        public event Action<bool> ChangeSelected;

        private InfentorySlotData _slotData;

        public InfentorySlot(InfentorySlotData slotData)
        {
            _slotData = slotData;
        }

        public bool IsSelected
        {
            get => Selected;
            set
            {
                Selected = value;
                ChangeSelected?.Invoke(value);
            }
        }

        public Sprite Sprite
        {
            get => _slotData.Sprite;
            set
            {
                if (_slotData.Sprite != value)
                {
                    _slotData.Sprite = value;
                    SpriteChanged?.Invoke(value);
                }
            }
        }

        public string ItemID
        {
            get => _slotData.ItemID;
            set
            {
                if (_slotData.ItemID != value)
                {
                    _slotData.ItemID = value;
                    ItemIDChanfed?.Invoke(value);
                }
            }
        }

        public int Amount
        {
            get => _slotData.Amount;
            set
            {
                if (_slotData.Amount != value)
                {
                    _slotData.Amount = value;
                    ItemAmountchanged?.Invoke(value);
                }
            }
        }
    }
}
