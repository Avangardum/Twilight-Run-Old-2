using System;
using UnityEngine;
using UnityEngine.UI;

namespace Avangardum.TwilightRun
{
    public class GameUI : MonoBehaviour, IGameUI 
    {
        [SerializeField] private GameObject _gameOverWindow;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        
        public event EventHandler RestartButtonClick;
        public event EventHandler MenuButtonPressed;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void ShowGameOverWindow()
        {
            _gameOverWindow.SetActive(true);
        }

        public void HideGameOverWindow()
        {
            _gameOverWindow.SetActive(false);
        }

        private void Awake()
        {
            _restartButton.onClick.AddListener(() => RestartButtonClick?.Invoke(this, EventArgs.Empty));
            _menuButton.onClick.AddListener(() => MenuButtonPressed?.Invoke(this, EventArgs.Empty));
        }
    }
}