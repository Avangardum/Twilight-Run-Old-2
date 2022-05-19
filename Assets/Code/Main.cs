using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private StandardLevelGenerator _standardLevelGenerator;
        [SerializeField] private TutorialLevelGenerator _tutorialLevelGenerator;
        [SerializeField] private PlayerCharactersController _playerCharactersController;
        [SerializeField] private PCInputManager _pcInputManager;
        [SerializeField] private MobileInputManager _mobileInputManager;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private ShopMenu _shopMenu;
        [SerializeField] private MenuManager _menuManager;
        [SerializeField] private CosmeticsManager _cosmeticsManager;
        [SerializeField] private ShopManager _shopManager;

        private void Awake()
        {
            // inject dependencies
            _gameManager.InjectDependencies(_standardLevelGenerator, _tutorialLevelGenerator, _playerCharactersController, _pcInputManager);
            _menuManager.InjectDependencies(_gameManager, _mainMenu, _shopMenu);
            _shopManager.InjectDependencies(_shopMenu, _cosmeticsManager);
        }
    }
}