using Infentory.View;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Infentory
{
    public class SelectButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private InfentorySlotView _inventorySlotView;
        [SerializeField] private int _index;

        public event Action<int> OnChangeSlotViewSelected;

        private void Start()
        {
            _button.onClick.AddListener(() => ChangeSlotViewSelected());
        }

        private void ChangeSlotViewSelected()
        {
            _inventorySlotView.IsSelected = !_inventorySlotView.IsSelected;
            OnChangeSlotViewSelected?.Invoke(_index);
        }
    }
}
