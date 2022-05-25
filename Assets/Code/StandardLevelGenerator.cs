using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class StandardLevelGenerator : MonoBehaviour, ILevelGenerator
    {
        [SerializeField] private GameObject _tracksSegmentPrefab;
        [SerializeField] private float _tracksSegmentLength;
        
        private List<GameObject> _levelElements = new List<GameObject>();
        private float _generationFrontZ;
        private float _trackFrontZ;
        private float _elementsFrontZ;
        private bool _isZeroTrackSegmentGenerated;
        
        public void GenerateToThePoint(float point)
        {
            // generate zero track segment if not exists
            if (!_isZeroTrackSegmentGenerated)
            {
                var zeroTrackSegment = Instantiate(_tracksSegmentPrefab, new Vector3(0, 0, -_tracksSegmentLength), Quaternion.identity);
                _levelElements.Add(zeroTrackSegment);
            }
            
            // generate tracks segments
            for (; _trackFrontZ < point; _trackFrontZ += _tracksSegmentLength)
            {
                var trackSegment = Instantiate(_tracksSegmentPrefab, new Vector3(0, 0, _trackFrontZ), Quaternion.identity);
                _levelElements.Add(trackSegment);
            }
            
            // generate elements
        }

        public void ClearBehindThePoint(float point)
        {
            foreach (var element in _levelElements.ToList())
            {
                float frontZ = element.CompareTag("TrackSegment") ? element.transform.position.z + _tracksSegmentLength : element.transform.position.z;
                if (frontZ < point)
                {
                    Destroy(element);
                    _levelElements.Remove(element);
                }
            }
        }

        public void ClearAll()
        {
            foreach (var element in _levelElements)
            {
                Destroy(element);
            }
            _levelElements.Clear();
            _generationFrontZ = 0;
            _trackFrontZ = 0;
            _elementsFrontZ = 0;
            _isZeroTrackSegmentGenerated = false;
        }
    }
}