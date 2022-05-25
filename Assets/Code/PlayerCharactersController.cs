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
        
        private enum SwappingState
        {
            None = 0,
            Jumping = 1,
            Falling = 2,
            Landing = 3,
        }

        private static readonly int AnimatorSpeedMultiplierHash = Animator.StringToHash("SpeedMultiplier");
        private static readonly int AnimatorSwappingStateHash = Animator.StringToHash("SwappingState");
        
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

        private bool IsSwappingPositions => _charactersPositions is CharactersPositions.WhiteMovingDown or CharactersPositions.WhiteMovingUp;
        private float SwapJumpingDuration => _config.PlayerCharacterBaseJumpingDuration / _speedMultiplier;
        private float SwapFallingDuration => _config.PlayerCharacterBaseFallingDuration / _speedMultiplier;
        private float SwapLandingDuration => _config.PlayerCharacterBaseLandingDuration / _speedMultiplier;
        private float TotalSwapDuration => SwapJumpingDuration + SwapFallingDuration + SwapLandingDuration;

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
            _whiteCharacterAnimator.enabled = true;
            _blackCharacterAnimator.enabled = true;
            _whiteCharacterAnimator.SetInteger(AnimatorSwappingStateHash, (int) SwappingState.None);
            _blackCharacterAnimator.SetInteger(AnimatorSwappingStateHash, (int) SwappingState.None);
            _isGameActive = true;
        }
        
        public void SwapCharacters()
        {
            if (!_isGameActive || IsSwappingPositions)
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
        }

        private void Awake()
        {
            _whiteCharacterAnimator = _whiteCharacter.GetComponent<Animator>();
            _blackCharacterAnimator = _blackCharacter.GetComponent<Animator>();
            _whiteCharacterStartPosition = _whiteCharacter.transform.position;
            _blackCharacterStartPosition = _blackCharacter.transform.position;
            _bottomTrackY = _whiteCharacterStartPosition.y;
            _topTrackY = _blackCharacterStartPosition.y;
            _whiteCharacter.GetComponent<PlayerCharacterCollisionDetector>().ObstacleCollision += OnWhiteCharacterObstacleCollision;
            _blackCharacter.GetComponent<PlayerCharacterCollisionDetector>().ObstacleCollision += OnBlackCharacterObstacleCollision;
        }

        private void OnWhiteCharacterObstacleCollision(object sender, PlayerCharacterCollisionArgs args)
        {
            if (args.OtherColor != GameColor.White)
            {
                KillCharacters();
            }
        }

        private void OnBlackCharacterObstacleCollision(object sender, PlayerCharacterCollisionArgs args)
        {
            if (args.OtherColor != GameColor.Black)
            {
                KillCharacters();
            }
        }

        private void KillCharacters()
        {
            _isGameActive = false;
            _whiteCharacterAnimator.enabled = false;
            _blackCharacterAnimator.enabled = false;
            CharacterDied?.Invoke(this, EventArgs.Empty);
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
                var positionLerpArgument = (_timeSinceSwapStart - SwapJumpingDuration) / SwapFallingDuration;
                
                // set vertical position
                var characterMovingUpPosition = _characterMovingUp.transform.position;
                characterMovingUpPosition.y = Mathf.Lerp(_bottomTrackY, _topTrackY, positionLerpArgument);
                _characterMovingUp.transform.position = characterMovingUpPosition;
                
                var characterMovingDownPosition = _characterMovingDown.transform.position;
                characterMovingDownPosition.y = Mathf.Lerp(_topTrackY, _bottomTrackY, positionLerpArgument);
                _characterMovingDown.transform.position = characterMovingDownPosition;

                // set euler angles
                var hasPassedTheMiddle = positionLerpArgument > 0.5f;
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
                
                // set animators state
                SwappingState swappingState;
                if (_timeSinceSwapStart < SwapJumpingDuration)
                {
                    swappingState = SwappingState.Jumping;
                }
                else if (_timeSinceSwapStart < SwapJumpingDuration + SwapFallingDuration)
                {
                    swappingState = SwappingState.Falling;
                }
                else
                {
                    swappingState = SwappingState.Landing;
                }
                _whiteCharacterAnimator.SetInteger(AnimatorSwappingStateHash, (int) swappingState);
                _blackCharacterAnimator.SetInteger(AnimatorSwappingStateHash, (int) swappingState);

                // check for swap end
                if (_timeSinceSwapStart >= TotalSwapDuration)
                {
                    // terminate swapping
                    _charactersPositions = _charactersPositions switch
                    {
                        CharactersPositions.WhiteMovingUp => CharactersPositions.WhiteOnTopTrack,
                        CharactersPositions.WhiteMovingDown => CharactersPositions.WhiteOnBottomTrack,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    _whiteCharacterAnimator.SetInteger(AnimatorSwappingStateHash, (int) SwappingState.None);
                    _blackCharacterAnimator.SetInteger(AnimatorSwappingStateHash, (int) SwappingState.None);
                }
            }

            // speed increase
            SpeedMultiplier += _config.PlayerCharacterSpeedMultiplierIncreaseSpeed * Time.fixedDeltaTime;
            SpeedMultiplier = Mathf.Clamp(SpeedMultiplier, 0, _config.PlayerCharacterSpeedMultiplierMax);
        }
    }
}