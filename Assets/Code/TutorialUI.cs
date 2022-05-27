using System;
using UnityEngine;
using UnityEngine.UI;

namespace Avangardum.TwilightRun
{
    class TutorialUI : MonoBehaviour, ITutorialUI
    {
        [SerializeField] private GameObject _hintWindow;
        [SerializeField] private Image _hintImage;
        [SerializeField] private Button _nextButton;
        
        public event EventHandler NextPressed;
        
        public void ShowHint(Sprite image)
        {
            _hintWindow.SetActive(true);
            _hintImage.sprite = image;
        }

        private void Awake()
        {
            _nextButton.onClick.AddListener(OnNextButtonClick);
        }

        private void OnNextButtonClick()
        {
            _hintWindow.SetActive(false);
            NextPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}