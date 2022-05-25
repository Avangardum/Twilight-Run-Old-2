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
        private bool _isGameActive;

        [SerializeField] private GameObject _whitePlayerCharacter;

        public void InjectDependencies(ILevelGenerator standardLevelGenerator, ILevelGenerator tutorialLevelGenerator, 
            IPlayerCharactersController playerCharactersController, IInputManager inputManager, IGameUI gameUI)
        {
            _standardLevelGenerator = standardLevelGenerator;
            _tutorialLevelGenerator = tutorialLevelGenerator;
            _playerCharactersController = playerCharactersController;
            _inputManager = inputManager;
            _gameUI = gameUI;

            inputManager.Tap += OnTap;
            playerCharactersController.CharacterDied += OnCharacterDied;
        }
        
        private void OnCharacterDied(object sender, EventArgs e)
        {
            _isGameActive = false;
            _gameUI.Show();
            _gameUI.ShowGameOverWindow();
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
            _gameUI.HideGameOverWindow();
        }

        public void StartTutorial()
        {
            throw new NotImplementedException();
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