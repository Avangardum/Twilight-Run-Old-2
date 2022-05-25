using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    [Serializable]
    public class LevelElementData
    {
        [field: SerializeField] public string Name { get; [UsedImplicitly] private set; }
        [field: SerializeField] public GameObject Prefab { get; [UsedImplicitly] private set; }
        [field: SerializeField] public float Length { get; [UsedImplicitly] private set; }
        [field: SerializeField] public float Difficulty { get; [UsedImplicitly] private set; }
    }
}