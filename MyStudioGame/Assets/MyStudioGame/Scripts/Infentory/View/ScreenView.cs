using UnityEngine;

namespace Infentory.View
{
    public class ScreenView : MonoBehaviour
    {
        [SerializeField] private InfentoryView _infentoryView;

        public InfentoryView InventoryView => _infentoryView;
    }
}
