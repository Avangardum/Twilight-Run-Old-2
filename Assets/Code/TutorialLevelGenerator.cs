using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class TutorialLevelGenerator : MonoBehaviour, ILevelGenerator
    {
        [SerializeField] private GameObject _tutorialLevelPrefab;
        private GameObject _tutorialLevelInstance;
        private bool _isTutorialLevelInstancePresent;

        public void GenerateToThePoint(float point)
        {
            if (_isTutorialLevelInstancePresent)
            {
                return;
            }

            Instantiate(_tutorialLevelPrefab);
        }

        public void ClearBehindThePoint(float point)
        {
            // nothing
        }

        public void ClearAll()
        {
            if (_isTutorialLevelInstancePresent)
            {
                Destroy(_tutorialLevelInstance);
                _isTutorialLevelInstancePresent = false;
            }
        }

        public void InjectDependencies(ILevelGenerationConfig config)
        {
            // nothing
        }
    }
}