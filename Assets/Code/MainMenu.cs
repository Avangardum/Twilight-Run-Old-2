using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class MainMenu : MonoBehaviour, IMainMenu
    {
        public event EventHandler ShopButtonPressed;
        public event EventHandler PlayButtonPressed;
        
        public void Show()
        {
            throw new NotImplementedException();
        }

        public void Hide()
        {
            throw new NotImplementedException();
        }
    }
}