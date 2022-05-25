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
        private bool _isGameActive;

        [SerializeField] private GameObject _whitePlayerCharacter;
        
        public void InjectDependencies(ILevelGenerator standardLevelGenerator, ILevelGenerator tutorialLevelGenerator, 
            IPlayerCharactersController playerCharactersController, IInputManager inputManager)
        {
            _standardLevelGenerator = standardLevelGenerator;
            _tutorialLevelGenerator = tutorialLevelGenerator;
            _playerCharactersController = playerCharactersController;
            _inputManager = inputManager;

            inputManager.Tap += OnTap;
            playerCharactersController.CharacterDied += OnCharacterDied;
        }

        private void OnCharacterDied(object sender, EventArgs e)
        {
            _isGameActive = false;
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
        }

        public void StartTutorial()
        {
            throw new NotImplementedException();
        }

        private void Update()
        {
            _currentLevelGenerator.GenerateToThePoint(_whitePlayerCharacter.transform.position.z + LevelGenerationRange);
            _currentLevelGenerator.ClearBehindThePoint(_whitePlayerCharacter.transform.position.z - LevelCleanupRange);
        }
    }
}