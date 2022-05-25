using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class ShopMenu : MonoBehaviour, IShopMenu
    {
        public event EventHandler BackButtonPressed;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}