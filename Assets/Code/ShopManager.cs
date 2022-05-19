using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class ShopManager : MonoBehaviour
    {
        private IShopMenu _shopMenu;
        private ICosmeticsManager _cosmeticsManager;

        public void InjectDependencies(IShopMenu shopMenu, ICosmeticsManager cosmeticsManager)
        {
            _shopMenu = shopMenu;
            _cosmeticsManager = cosmeticsManager;
        }
    }
}