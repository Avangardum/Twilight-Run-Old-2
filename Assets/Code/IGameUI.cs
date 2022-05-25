using System;

namespace Avangardum.TwilightRun
{
    public interface IGameUI : IUIScreen
    {
        event EventHandler RestartButtonClick;
        event EventHandler MenuButtonPressed;
        
        void ShowGameOverWindow();
        void HideGameOverWindow();
    }
}