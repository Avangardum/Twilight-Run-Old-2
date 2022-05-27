namespace Avangardum.TwilightRun
{
    public interface IGameManager
    {
        void InjectDependencies(ILevelGenerator standardLevelGenerator, ILevelGenerator tutorialLevelGenerator, 
            IPlayerCharactersController playerCharactersController, IInputManager inputManager, IGameUI gameUI, ICoinStorage coinStorage,
            ITutorialManager tutorialManager);
        void StartGame();
        void StartTutorial();
    }
}