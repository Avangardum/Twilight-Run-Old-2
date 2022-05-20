using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class PlayerCharactersController : MonoBehaviour, IPlayerCharactersController
    {
        private static readonly int AnimatorSpeedMultiplierHash = Animator.StringToHash("SpeedMultiplier");
        
        [SerializeField] private GameObject _whiteCharacter;
        [SerializeField] private GameObject _blackCharacter;

        private Animator _whiteCharacterAnimator;
        private Animator _blackCharacterAnimator;
        private Vector3 _whiteCharacterStartPosition;
        private Vector3 _blackCharacterStartPosition;
        private IPlayerCharactersConfig _config;
        private bool _isGameActive;
        private float _speedMultiplier;

        public float SpeedMultiplier
        {
            get => _speedMultiplier;
            private set
            {
                _speedMultiplier = value;
                _whiteCharacterAnimator.SetFloat(AnimatorSpeedMultiplierHash, value);
                _blackCharacterAnimator.SetFloat(AnimatorSpeedMultiplierHash, value);
            }
        }

        public event EventHandler CoinCollected;
        public event EventHandler CharacterDied;
        
        public void Reset()
        {
            _whiteCharacter.transform.position = _whiteCharacterStartPosition;
            _blackCharacter.transform.position = _blackCharacterStartPosition;
            SpeedMultiplier = 1;
            _isGameActive = true;
        }
        
        public void SwapCharacters()
        {
            throw new NotImplementedException();
        }

        public void InjectDependencies(IPlayerCharactersConfig config)
        {
            _config = config;
        }

        private void Awake()
        {
            _whiteCharacterAnimator = _whiteCharacter.GetComponent<Animator>();
            _blackCharacterAnimator = _blackCharacter.GetComponent<Animator>();
            _whiteCharacterStartPosition = _whiteCharacter.transform.position;
            _blackCharacterStartPosition = _blackCharacter.transform.position;
        }

        private void FixedUpdate()
        {
            if (!_isGameActive)
            {
                return;
            }
            
            var horizontalSpeed = _config.PlayerCharacterBaseHorizontalSpeed * SpeedMultiplier;
            var translation = Vector3.forward * (horizontalSpeed * Time.fixedDeltaTime);
            _whiteCharacter.transform.Translate(translation, Space.World);
            _blackCharacter.transform.Translate(translation, Space.World);

            SpeedMultiplier += _config.PlayerCharacterSpeedMultiplierIncreaseSpeed * Time.fixedDeltaTime;
            SpeedMultiplier = Mathf.Clamp(SpeedMultiplier, 0, _config.PlayerCharacterSpeedMultiplierMax);
        }
    }
}