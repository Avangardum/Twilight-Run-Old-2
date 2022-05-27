using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class GameManager : MonoBehaviour, IGameManager
    {
        private const float LevelGenerationRange = 20;
        private const float LevelCleanupRange = 5;
        
        private ILevelGenerator _standardLevelGenerator;
        private ILevelGenerator _tutorialLevelGenerator;
        private ILevelGenerator _currentLevelGenerator;
        private IPlayerCharactersController _playerCharactersController;
        private IInputManager _inputManager;
        private IGameUI _gameUI;
        private ICoinStorage _coinStorage;
        private bool _isGameActive;
        private bool _isTutorial;
        private ITutorialManager _tutorialManager;

        [SerializeField] private GameObject _whitePlayerCharacter;

        public void InjectDependencies(ILevelGenerator standardLevelGenerator, ILevelGenerator tutorialLevelGenerator, 
            IPlayerCharactersController playerCharactersController, IInputManager inputManager, IGameUI gameUI, ICoinStorage coinStorage,
            ITutorialManager tutorialManager)
        {
            _standardLevelGenerator = standardLevelGenerator;
            _tutorialLevelGenerator = tutorialLevelGenerator;
            _playerCharactersController = playerCharactersController;
            _inputManager = inputManager;
            _gameUI = gameUI;
            _coinStorage = coinStorage;
            _tutorialManager = tutorialManager;

            inputManager.Tap += OnTap;
            playerCharactersController.CharacterDied += OnCharacterDied;
            playerCharactersController.CoinCollected += OnCoinCollected;
            tutorialManager.TutorialCleared += OnTutorialCleared;
        }

        private void OnTutorialCleared(object sender, EventArgs e)
        {
            _playerCharactersController.SetIsGameActive(false);
        }

        private void OnCoinCollected(object sender, EventArgs e)
        {
            _coinStorage.AddCoins(1);
        }

        private void OnCharacterDied(object sender, EventArgs e)
        {
            _isGameActive = false;
            if (!_isTutorial)
            {
                _gameUI.Show();
                _gameUI.ShowGameOverWindow();
            }
            else
            {
                StartTutorial();
            }
        }

        private void OnTap(object sender, EventArgs e)
        {
            _playerCharactersController.SwapCharacters();
        }

        public void StartGame()
        {
            _playerCharactersController.Reset();
            _currentLevelGenerator = _standardLevelGenerator;
            _currentLevelGenerator.ClearAll();
            _isGameActive = true;
            _isTutorial = false;
            _tutorialManager.Disable();
            _gameUI.HideGameOverWindow();
        }

        public void StartTutorial()
        {
            _playerCharactersController.Reset();
            _currentLevelGenerator = _tutorialLevelGenerator;
            _currentLevelGenerator.ClearAll();
            _isGameActive = true;
            _isTutorial = true;
            _tutorialManager.Enable();
            _gameUI.HideGameOverWindow();
        }

        private void Update()
        {
            if (!_isGameActive)
            {
                return;
            }
            
            _currentLevelGenerator.GenerateToThePoint(_whitePlayerCharacter.transform.position.z + LevelGenerationRange);
            _currentLevelGenerator.ClearBehindThePoint(_whitePlayerCharacter.transform.position.z - LevelCleanupRange);
        }
    }
}