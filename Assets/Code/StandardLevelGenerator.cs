using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class StandardLevelGenerator : MonoBehaviour, ILevelGenerator
    {
        [SerializeField] private GameObject _tracksSegmentPrefab;
        [SerializeField] private float _tracksSegmentLength;
        [SerializeField] private LevelElementsList _levelElementsList;
        
        private readonly List<GameObject> _levelObjects = new List<GameObject>();
        private float _trackFrontZ;
        private float _elementsFrontZ;
        private bool _isZeroTrackSegmentGenerated;
        private ILevelGenerationConfig _config;
        private float _currentElementsDifficulty;

        public void GenerateToThePoint(float point)
        {
            // generate zero track segment if not exists
            if (!_isZeroTrackSegmentGenerated)
            {
                var zeroTrackSegment = Instantiate(_tracksSegmentPrefab, new Vector3(0, 0, -_tracksSegmentLength), Quaternion.identity);
                _levelObjects.Add(zeroTrackSegment);
            }
            
            // generate tracks segments
            for (; _trackFrontZ < point; _trackFrontZ += _tracksSegmentLength)
            {
                var trackSegment = Instantiate(_tracksSegmentPrefab, new Vector3(0, 0, _trackFrontZ), Quaternion.identity);
                _levelObjects.Add(trackSegment);
            }
            
            // generate elements
            while (_elementsFrontZ < point)
            {
                var element = _levelElementsList.GetRandomElement(_currentElementsDifficulty);
                var elementGO = Instantiate(element.Prefab, new Vector3(0, 0, _elementsFrontZ), Quaternion.identity);
                _levelObjects.Add(elementGO);
                _elementsFrontZ += element.Length + _config.LevelElementsGapLength;
            }
        }

        public void ClearBehindThePoint(float point)
        {
            foreach (var element in _levelObjects.ToList())
            {
                float frontZ = element.CompareTag("TrackSegment") ? element.transform.position.z + _tracksSegmentLength : element.transform.position.z;
                if (frontZ < point)
                {
                    Destroy(element);
                    _levelObjects.Remove(element);
                }
            }
        }

        public void ClearAll()
        {
            foreach (var element in _levelObjects)
            {
                Destroy(element);
            }
            _levelObjects.Clear();
            _trackFrontZ = 0;
            _elementsFrontZ = _config.BeginningEmptinessLength;
            _isZeroTrackSegmentGenerated = false;
            _currentElementsDifficulty = 0;
        }

        public void InjectDependencies(ILevelGenerationConfig config)
        {
            _config = config;
        }

        private void FixedUpdate()
        {
            _currentElementsDifficulty += _config.ElementsDifficultyIncreaseSpeed * Time.fixedDeltaTime;
        }
    }
}