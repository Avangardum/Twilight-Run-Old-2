using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class GameManager : MonoBehaviour, IGameManager
    {
        private ILevelGenerator _standardLevelGenerator;
        private ILevelGenerator _tutorialLevelGenerator;
        private IPlayerCharactersController _playerCharactersController;
        private IInputManager _inputManager;

        public void InjectDependencies(ILevelGenerator standardLevelGenerator, ILevelGenerator tutorialLevelGenerator, 
            IPlayerCharactersController playerCharactersController, IInputManager inputManager)
        {
            _standardLevelGenerator = standardLevelGenerator;
            _tutorialLevelGenerator = tutorialLevelGenerator;
            _playerCharactersController = playerCharactersController;
            _inputManager = inputManager;

            inputManager.Tap += OnTap;
        }

        private void OnTap(object sender, EventArgs e)
        {
            _playerCharactersController.SwapCharacters();
        }

        public void StartGame()
        {
            _playerCharactersController.Reset();
        }

        public void StartTutorial()
        {
            throw new System.NotImplementedException();
        }
    }
}