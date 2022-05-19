using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class MenuManager : MonoBehaviour
    {
        private IGameManager _gameManager;
        private IMainMenu _mainMenu;
        private IShopMenu _shopMenu;

        public void InjectDependencies(IGameManager gameManager, IMainMenu mainMenu, IShopMenu shopMenu)
        {
            _gameManager = gameManager;
            _mainMenu = mainMenu;
            _shopMenu = shopMenu;
        }
    }
}