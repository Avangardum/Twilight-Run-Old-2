using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class CoinStorage : MonoBehaviour, ICoinStorage
    {
        private const string PlayerPrefsCoinsKey = "Coins";
        
        public int Coins
        {
            get => PlayerPrefs.GetInt(PlayerPrefsCoinsKey, 0);
            private set => PlayerPrefs.SetInt(PlayerPrefsCoinsKey, value);
        }
        
        public void AddCoins(int amount)
        {
            Coins += amount;
        }

        public void TakeCoins(int amount)
        {
            if (amount > Coins)
            {
                throw new InvalidOperationException("Not enough coins");
            }

            Coins -= amount;
        }
    }
}