using Infentory.ReadOnly;
using UnityEngine;

namespace Infentory
{
    public class InfentoryGridView : MonoBehaviour
    {
        private IReadOnlyInfentoryGrid _inventory;

        public void Setup(IReadOnlyInfentoryGrid inventory)
        {
            _inventory = inventory;
            Print();
        }

        public void Print()
        {
            var slots = _inventory.GetSlots();
            var size = _inventory.Size;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var slot = slots[i, j];

                    Debug.Log($"slots {i},{j}. ID {slot.ItemID}, Amount {slot.Amount}");
                }
            }
        }
    }
}
