using System;

namespace Avangardum.TwilightRun
{
    public interface IShopMenu : IUIScreen
    {
        event EventHandler BackButtonPressed;
    }
}