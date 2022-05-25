using System.Collections.Generic;
using System.Linq;
using Avangardum.CSharpUtilityLib;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    [CreateAssetMenu(menuName = "Level Elements List")]
    public class LevelElementsList : ScriptableObject
    {
        [SerializeField] private List<LevelElementData> _elements;
        
        public LevelElementData GetRandomElement(float maxDifficulty)
        {
            return _elements.Where(x => x.Difficulty <= maxDifficulty).Random();
        }
    }
}