using Infentory.ReadOnly;
using Infentory.View;
using System.Collections.Generic;

namespace Infentory.Contraller
{
    public class InfentoryGridContraller
    {
        private readonly List<InventorySlotController> _slotcontaroller = new();

        public InfentoryGridContraller(IReadOnlyInfentoryGrid inventory, InfentoryView view)
        {
            var size = inventory.Size;
            var slots = inventory.GetSlots();
            var lineLenght = size.y;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var index = i * lineLenght + j;
                    var slotView = view.GetInfentorySlotView(index);
                    var slot = slots[i, j];

                    _slotcontaroller.Add(new InventorySlotController(slot, slotView));
                }
            }
        }
    }
}
