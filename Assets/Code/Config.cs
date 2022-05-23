using JetBrains.Annotations;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    [CreateAssetMenu(menuName = "Config")]
    public class Config : ScriptableObject, IPlayerCharactersConfig
    {
        [field: SerializeField] public float PlayerCharacterBaseHorizontalSpeed { get; [UsedImplicitly] private set; }
        [field: SerializeField] public float PlayerCharacterBaseVerticalSpeed { get; [UsedImplicitly] private set; }
        [field: SerializeField] public float PlayerCharacterSpeedMultiplierIncreaseSpeed { get; [UsedImplicitly] private set; }
        [field: SerializeField] public float PlayerCharacterSpeedMultiplierMax { get; [UsedImplicitly] private set; }
        [field: SerializeField] public float PlayerCharacterJumpingDuration { get; [UsedImplicitly] private set; }
        [field: SerializeField] public float PlayerCharacterFallingDuration { get; [UsedImplicitly] private set; }
        [field: SerializeField] public float PlayerCharacterLandingDuration { get; [UsedImplicitly] private set; }
    }
}