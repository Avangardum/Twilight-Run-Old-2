using System;

namespace Avangardum.TwilightRun
{
    public interface IPlayerCharactersController
    {
        event EventHandler CoinCollected;
        event EventHandler CharacterDied;
        void Reset();
        void SwapCharacters();
        void InjectDependencies(IPlayerCharactersConfig config);
    }
}