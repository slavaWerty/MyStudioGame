using System;
using UnityEngine;

namespace Infentory.ReadOnly
{
    public interface IReadOnlyInfentorySlot
    {
        event Action<string> ItemIDChanfed;
        event Action<int> ItemAmountchanged;
        event Action<Sprite> SpriteChanged;
        event Action<bool> ChangeSelected;

        string ItemID { get; }
        int Amount { get; }
        Sprite Sprite { get; }
        bool isEmpty { get; }
        bool IsSelected { get; }
    }
}
