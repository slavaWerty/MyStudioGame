using System;
using UnityEngine;

namespace Infentory.ReadOnly
{
    public interface IReadOnlyInfentoryGrid : IReadOnlyInfentory
    {
        event Action<Vector2Int> SizeChanged;

        Vector2Int Size { get; }

        IReadOnlyInfentorySlot[,] GetSlots();
    }
}
