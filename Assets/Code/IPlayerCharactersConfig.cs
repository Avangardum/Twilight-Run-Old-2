namespace Avangardum.TwilightRun
{
    public interface IPlayerCharactersConfig
    {
        float PlayerCharacterBaseHorizontalSpeed { get; }
        float PlayerCharacterBaseVerticalSpeed { get; }
        float PlayerCharacterSpeedMultiplierIncreaseSpeed { get; }
        float PlayerCharacterSpeedMultiplierMax { get; }
    }
}