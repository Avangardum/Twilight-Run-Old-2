namespace Avangardum.TwilightRun
{
    public interface IGameManager
    {
        void InjectDependencies(ILevelGenerator standardLevelGenerator, ILevelGenerator tutorialLevelGenerator, 
            IPlayerCharactersController playerCharactersController, IInputManager inputManager, IGameUI gameUI, ICoinStorage coinStorage);
        void StartGame();
        void StartTutorial();
    }
}