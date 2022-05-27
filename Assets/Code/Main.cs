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
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private MenuManager _menuManager;
        [SerializeField] private CosmeticsManager _cosmeticsManager;
        [SerializeField] private ShopManager _shopManager;
        [SerializeField] private CoinStorage _coinStorage;
        [SerializeField] private Config _config;
        [SerializeField] private TutorialManager _tutorialManager;
        [SerializeField] private TutorialUI _tutorialUI;

        private void Awake()
        {
            // inject dependencies
            _gameManager.InjectDependencies(_standardLevelGenerator, _tutorialLevelGenerator, _playerCharactersController, 
                _pcInputManager, _gameUI, _coinStorage, _tutorialManager);
            _playerCharactersController.InjectDependencies(_config);
            _standardLevelGenerator.InjectDependencies(_config);
            _menuManager.InjectDependencies(_gameManager, _mainMenu, _shopMenu, _gameUI, _tutorialManager);
            _shopManager.InjectDependencies(_shopMenu, _cosmeticsManager);
            _tutorialManager.Initialize(_config, _tutorialUI);

            _gameManager.StartTutorial();
        }
    }
}