using System;

namespace Avangardum.TwilightRun
{
    public interface IMainMenu : IMenu
    {
        event EventHandler ShopButtonPressed;
        event EventHandler PlayButtonPressed;
    }
}