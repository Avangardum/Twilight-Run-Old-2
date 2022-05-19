using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class PlayerCharactersController : MonoBehaviour, IPlayerCharactersController
    {
        public event EventHandler CoinCollected;
        public event EventHandler CharacterDied;
        
        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void SetSpeed(float speed)
        {
            throw new NotImplementedException();
        }

        public void SwapCharacters()
        {
            throw new NotImplementedException();
        }
    }
}