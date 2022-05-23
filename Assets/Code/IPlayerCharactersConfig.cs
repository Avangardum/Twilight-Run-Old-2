namespace Avangardum.TwilightRun
{
    public interface IPlayerCharactersConfig
    {
        float PlayerCharacterBaseHorizontalSpeed { get; }
        float PlayerCharacterBaseVerticalSpeed { get; }
        float PlayerCharacterSpeedMultiplierIncreaseSpeed { get; }
        float PlayerCharacterSpeedMultiplierMax { get; }
        float PlayerCharacterJumpingDuration { get; }
        float PlayerCharacterFallingDuration { get; }
        float PlayerCharacterLandingDuration { get; }
    }
}