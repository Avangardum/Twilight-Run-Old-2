namespace Avangardum.TwilightRun
{
    public interface IGameManager
    {
        void InjectDependencies(ILevelGenerator standardLevelGenerator, ILevelGenerator tutorialLevelGenerator, 
            IPlayerCharactersController playerCharactersController, IInputManager inputManager);
        void StartGame();
        void StartTutorial();
    }
}