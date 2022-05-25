using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class TutorialLevelGenerator : MonoBehaviour, ILevelGenerator
    {
        public void GenerateToThePoint(float point)
        {
            throw new System.NotImplementedException();
        }

        public void ClearBehindThePoint(float point)
        {
            throw new System.NotImplementedException();
        }

        public void ClearAll()
        {
            throw new System.NotImplementedException();
        }

        public void InjectDependencies(ILevelGenerationConfig config)
        {
            
        }
    }
}