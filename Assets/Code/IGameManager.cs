namespace Avangardum.TwilightRun
{
    public interface IGameManager
    {
        void InjectDependencies(ILevelGenerator standardLevelGenerator, ILevelGenerator tutorialLevelGenerator, 
            IPlayerCharactersController playerCharactersController, IInputManager inputManager, IGameUI gameUI);
        void StartGame();
        void StartTutorial();
    }
}