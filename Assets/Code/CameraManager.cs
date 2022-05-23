using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Avangardum.TwilightRun
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private GameObject _whitePlayerCharacter;
        [SerializeField] private GameObject _blackPlayerCharacter;

        private float _cameraHeight;
        private float _cameraZOffset;
        private Transform _cameraTransform;

        private void Awake()
        {
            // ReSharper disable once Unity.NoNullPropagation
            _cameraTransform = Camera.main?.transform;
            Assert.IsNotNull(_cameraTransform);
            var position = _cameraTransform.position;
            _cameraHeight = position.y;
            _cameraZOffset = position.z - _whitePlayerCharacter.transform.position.z;
        }

        private void Update()
        {
            var position = _cameraTransform.transform.position;
            position.z = _whitePlayerCharacter.transform.position.z + _cameraZOffset;
            _cameraTransform.position = position;
        }
    }
}