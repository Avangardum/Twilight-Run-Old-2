using System;

namespace Avangardum.TwilightRun
{
    public interface IMainMenu : IUIScreen
    {
        event EventHandler ShopButtonPressed;
        event EventHandler PlayButtonPressed;
    }
}