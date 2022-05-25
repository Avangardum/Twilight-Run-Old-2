using System;
using UnityEngine;
using UnityEngine.UI;

namespace Avangardum.TwilightRun
{
    public class MainMenu : MonoBehaviour, IMainMenu
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _shopButton;
        
        public event EventHandler ShopButtonPressed;
        public event EventHandler PlayButtonPressed;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            _playButton.onClick.AddListener(() => PlayButtonPressed?.Invoke(this, EventArgs.Empty));
            _shopButton.onClick.AddListener(() => ShopButtonPressed?.Invoke(this, EventArgs.Empty));
        }
    }
}