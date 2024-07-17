using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infentory.View
{
    public class InfentorySlotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textTitle;
        [SerializeField] private TMP_Text _textAmount;
        [SerializeField] private Image _imageSprite;
        [SerializeField] private Image _backGround;

        public bool IsSelected;

        private void Update()
        {
            if (IsSelected)
                _backGround.color = Color.yellow;
            else
                _backGround.color = Color.green;
        }

        public string TextTitle
        {
            get => _textTitle.text;
            set => _textTitle.text = value;
        }

        public Sprite Sprite
        {
            get => _imageSprite.sprite;
            set => _imageSprite.sprite = value;
        }

        public int Amount
        {
            get => Convert.ToInt32(_textAmount.text);
            set => _textAmount.text = value == 0 ? "" : value.ToString();
        }
    }
}
