using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class MenuManager : MonoBehaviour
    {
        private IGameManager _gameManager;
        private IMainMenu _mainMenu;
        private IShopMenu _shopMenu;
        private IGameUI _gameUI;
        private ITutorialManager _tutorialManager;

        public void InjectDependencies(IGameManager gameManager, IMainMenu mainMenu, IShopMenu shopMenu, IGameUI gameUI, ITutorialManager tutorialManager)
        {
            _gameManager = gameManager;
            _mainMenu = mainMenu;
            _shopMenu = shopMenu;
            _gameUI = gameUI;
            _tutorialManager = tutorialManager;

            _mainMenu.PlayButtonPressed += OnPlayButtonPressed;
            _mainMenu.ShopButtonPressed += OnShopButtonPressed;
            _gameUI.MenuButtonPressed += OnMenuButtonPressed;
            _gameUI.RestartButtonClick += OnRestartGameButtonPressed;
            tutorialManager.TutorialCleared += OnTutorialCleared;
        }

        private void OnTutorialCleared(object sender, EventArgs e)
        {
            HideAllUIScreens();
            _mainMenu.Show();
        }

        private void OnMenuButtonPressed(object sender, EventArgs e)
        {
            HideAllUIScreens();
            _mainMenu.Show();
        }

        private void OnRestartGameButtonPressed(object sender, EventArgs e)
        {
            HideAllUIScreens();
            _gameManager.StartGame();
        }

        private void OnPlayButtonPressed(object sender, EventArgs e)
        {
            HideAllUIScreens();
            _gameManager.StartGame();
        }

        private void OnShopButtonPressed(object sender, EventArgs e)
        {
            
        }

        private void HideAllUIScreens()
        {
            _mainMenu.Hide();
            _shopMenu.Hide();
            _gameUI.Hide();
        }

        private void Start()
        {
            HideAllUIScreens();
            if (_tutorialManager.IsTutorialCleared)
            {
                _mainMenu.Show();
            }
            else
            {
                _gameManager.StartTutorial();
            }
        }
    }
}