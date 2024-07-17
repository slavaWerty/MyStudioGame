using Infentory.View;

namespace Infentory.Contraller
{
    public class InfentoryController
    {
        private readonly InfentoryGrid _inventory;
        private readonly ScreenView _view;

        private InfentoryGridContraller _currentInventoryController;

        public InfentoryController(InfentoryGrid inventory, ScreenView view)
        {
            _inventory = inventory;
            _view = view;
        }

        public void OpenInventory()
        {
            var inventoryView = _view.InventoryView;

            _currentInventoryController = new InfentoryGridContraller(_inventory, inventoryView);
        }
    }
}
