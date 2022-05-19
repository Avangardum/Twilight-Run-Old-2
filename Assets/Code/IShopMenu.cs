using System;

namespace Avangardum.TwilightRun
{
    public interface IShopMenu : IMenu
    {
        event EventHandler BackButtonPressed;
    }
}