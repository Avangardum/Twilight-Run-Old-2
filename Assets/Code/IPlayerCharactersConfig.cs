namespace Avangardum.TwilightRun
{
    public interface IPlayerCharactersConfig
    {
        float PlayerCharacterBaseHorizontalSpeed { get; }
        float PlayerCharacterSpeedMultiplierIncreaseSpeed { get; }
        float PlayerCharacterSpeedMultiplierMax { get; }
        float PlayerCharacterBaseJumpingDuration { get; }
        float PlayerCharacterBaseFallingDuration { get; }
        float PlayerCharacterBaseLandingDuration { get; }
    }
}