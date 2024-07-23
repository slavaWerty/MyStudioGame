using Infentory.View;

namespace Infentory.Contraller
{
    public class InfentoryController
    {
        private readonly InfentoryService _inventory;
        private readonly ScreenView _view;

        private InfentoryGridContraller _currentInventoryController;

        public InfentoryController(InfentoryService inventory, ScreenView view)
        {
            _inventory = inventory;
            _view = view;
        }

        public void OpenInventory(string ownerId)
        {
            var infentory = _inventory.GetInventory(ownerId);
            var inventoryView = _view.InventoryView;

            _currentInventoryController = new InfentoryGridContraller(infentory, inventoryView);
        }
    }
}
