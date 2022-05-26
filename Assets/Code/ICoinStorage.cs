namespace Avangardum.TwilightRun
{
    public interface ICoinStorage
    {
        int Coins { get; }
        void AddCoins(int amount);
        void TakeCoins(int amount);
    }
}