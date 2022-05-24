using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class PlayerCharactersController : MonoBehaviour, IPlayerCharactersController
    {
        private static readonly Vector3 EulerAnglesOnBottomTrack = new Vector3(0, 0, 0);
        private static readonly Vector3 EulerAnglesOnTopTrack = new Vector3(180, 180, 0);
        private static readonly Vector3 EulerAnglesInMiddleMovingUp = new Vector3(180, 0, 0);
        private static readonly Vector3 EulerAnglesInMiddleMovingDown = new Vector3(0, 180, 0);
        
        private enum CharactersPositions
        {
            None = 0,
            WhiteOnBottomTrack = 1,
            WhiteOnTopTrack = 2,
            WhiteMovingUp = 3,
            WhiteMovingDown = 4,
        }

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
        private CharactersPositions _charactersPositions;
        private float _timeSinceSwapStart;
        private GameObject _characterMovingUp;
        private GameObject _characterMovingDown;
        private float _topTrackY;
        private float _bottomTrackY;
        private float _totalSwapDuration;

        private bool IsSwappingPositions => _charactersPositions is CharactersPositions.WhiteMovingDown or CharactersPositions.WhiteMovingUp;

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
            _whiteCharacter.transform.eulerAngles = EulerAnglesOnBottomTrack;
            _blackCharacter.transform.eulerAngles = EulerAnglesOnTopTrack;
            _charactersPositions = CharactersPositions.WhiteOnBottomTrack;
            SpeedMultiplier = 1;
            _isGameActive = true;
        }
        
        public void SwapCharacters()
        {
            if (IsSwappingPositions)
            {
                return;
            }

            switch (_charactersPositions)
            {
                case CharactersPositions.WhiteOnBottomTrack:
                    _charactersPositions = CharactersPositions.WhiteMovingUp;
                    _characterMovingUp = _whiteCharacter;
                    _characterMovingDown = _blackCharacter;
                    break;
                case CharactersPositions.WhiteOnTopTrack:
                    _charactersPositions = CharactersPositions.WhiteMovingDown;
                    _characterMovingUp = _blackCharacter;
                    _characterMovingDown = _whiteCharacter;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _timeSinceSwapStart = 0;
        }

        public void InjectDependencies(IPlayerCharactersConfig config)
        {
            _config = config;
            _totalSwapDuration = config.PlayerCharacterJumpingDuration + config.PlayerCharacterFallingDuration + config.PlayerCharacterLandingDuration;
        }

        private void Awake()
        {
            _whiteCharacterAnimator = _whiteCharacter.GetComponent<Animator>();
            _blackCharacterAnimator = _blackCharacter.GetComponent<Animator>();
            _whiteCharacterStartPosition = _whiteCharacter.transform.position;
            _blackCharacterStartPosition = _blackCharacter.transform.position;
            _bottomTrackY = _whiteCharacterStartPosition.y;
            _topTrackY = _blackCharacterStartPosition.y;
        }

        private void FixedUpdate()
        {
            if (!_isGameActive)
            {
                return;
            }

            // horizontal movement
            var horizontalSpeed = _config.PlayerCharacterBaseHorizontalSpeed * SpeedMultiplier;
            var translation = Vector3.forward * (horizontalSpeed * Time.fixedDeltaTime);
            _whiteCharacter.transform.Translate(translation, Space.World);
            _blackCharacter.transform.Translate(translation, Space.World);
            
            // swapping
            if (IsSwappingPositions)
            {
                _timeSinceSwapStart += Time.fixedDeltaTime;
                var positionLerpArgument = (_timeSinceSwapStart - _config.PlayerCharacterJumpingDuration) / _config.PlayerCharacterFallingDuration;
                
                // set vertical position
                var characterMovingUpPosition = _characterMovingUp.transform.position;
                characterMovingUpPosition.y = Mathf.Lerp(_bottomTrackY, _topTrackY, positionLerpArgument);
                _characterMovingUp.transform.position = characterMovingUpPosition;
                
                var characterMovingDownPosition = _characterMovingDown.transform.position;
                characterMovingDownPosition.y = Mathf.Lerp(_topTrackY, _bottomTrackY, positionLerpArgument);
                _characterMovingDown.transform.position = characterMovingDownPosition;

                // set euler angles
                var hasPassedTheMiddle = positionLerpArgument > 0.5f;
                // var rotationLerpArgument = hasPassedTheMiddle ? (positionLerpArgument * 2) - 1 : positionLerpArgument * 2;
                if (hasPassedTheMiddle)
                {
                    var rotationLerpArgument = positionLerpArgument * 2 - 1;
                    _characterMovingUp.transform.eulerAngles = Vector3.Lerp(EulerAnglesInMiddleMovingUp, EulerAnglesOnTopTrack, rotationLerpArgument);
                    _characterMovingDown.transform.eulerAngles = Vector3.Lerp(EulerAnglesInMiddleMovingDown, EulerAnglesOnBottomTrack, rotationLerpArgument);
                }
                else
                {
                    var rotationLerpArgument = positionLerpArgument * 2;
                    _characterMovingUp.transform.eulerAngles = Vector3.Lerp(EulerAnglesOnBottomTrack, EulerAnglesInMiddleMovingUp, rotationLerpArgument);
                    _characterMovingDown.transform.eulerAngles = Vector3.Lerp(EulerAnglesOnTopTrack, EulerAnglesInMiddleMovingDown, rotationLerpArgument);
                }

                if (_timeSinceSwapStart >= _totalSwapDuration)
                {
                    // terminate swapping
                    _charactersPositions = _charactersPositions switch
                    {
                        CharactersPositions.WhiteMovingUp => CharactersPositions.WhiteOnTopTrack,
                        CharactersPositions.WhiteMovingDown => CharactersPositions.WhiteOnBottomTrack,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
            }

            // speed increase
            SpeedMultiplier += _config.PlayerCharacterSpeedMultiplierIncreaseSpeed * Time.fixedDeltaTime;
            SpeedMultiplier = Mathf.Clamp(SpeedMultiplier, 0, _config.PlayerCharacterSpeedMultiplierMax);
        }
    }
}