using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infentory
{
    [Serializable]
    public class InfentoryGridData
    {
        public List<InfentorySlotData> Slots;
        public Vector2Int Size;
        public Sprite Sprite;
    }
}

