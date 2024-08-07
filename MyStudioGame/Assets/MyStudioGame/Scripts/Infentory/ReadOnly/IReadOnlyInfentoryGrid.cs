﻿using System;
using UnityEngine;

namespace Infentory.ReadOnly
{
    public interface IReadOnlyInfentoryGrid : IReadOnlyInfentory
    {
        event Action<Vector2Int> SizeChanged;

        public bool IsCrystalInfentory { get; }

        Vector2Int Size { get; }

        IReadOnlyInfentorySlot[,] GetSlots();
    }
}
