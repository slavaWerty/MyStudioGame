using UnityEngine;

namespace Infentory.View
{
    public class InfentoryView : MonoBehaviour
    {
        [SerializeField] private InfentorySlotView[] _slots;

        public InfentorySlotView GetInfentorySlotView(int index)
        {
            return _slots[index];
        }
    }
}
