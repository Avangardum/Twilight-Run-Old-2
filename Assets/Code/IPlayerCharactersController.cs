using System;

namespace Avangardum.TwilightRun
{
    public interface IPlayerCharactersController
    {
        event EventHandler CoinCollected;
        event EventHandler CharacterDied;
        void Reset();
        void SetSpeed(float speed);
        void SwapCharacters();
    }
}