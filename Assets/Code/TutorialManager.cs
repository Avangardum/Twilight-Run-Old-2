using System;
using System.Linq;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class TutorialManager : MonoBehaviour, ITutorialManager
    {
        private const string PlayerPrefsIsTutorialClearedKey = "IsTutorialCleared";
        
        [SerializeField] private GameObject _whitePlayerCharacter;
        
        private bool _isEnabled;
        private int _nextHintIndex;
        private float _nextHintPoint;
        private ITutorialConfig _config;
        private ITutorialUI _tutorialUI;

        public bool IsTutorialCleared
        {
            get => PlayerPrefs.GetInt(PlayerPrefsIsTutorialClearedKey) == 1;
            set => PlayerPrefs.SetInt(PlayerPrefsIsTutorialClearedKey, value ? 1 : 0);
        }
        
        public event EventHandler TutorialCleared;
        
        public void Disable()
        {
            _isEnabled = false;
        }

        public void Enable()
        {
            _isEnabled = true;
            _nextHintIndex = 0;
            _nextHintPoint = _config.TutorialHintData.First().Point;
        }

        public void Initialize(ITutorialConfig tutorialConfig, ITutorialUI tutorialUI)
        {
            _config = tutorialConfig;
            _tutorialUI = tutorialUI;
            _tutorialUI.NextPressed += OnNextPressed;
        }

        private void OnNextPressed(object sender, EventArgs e)
        {
            Time.timeScale = 1;
            if (_nextHintIndex != _config.TutorialHintData.Count - 1)
            {
                _nextHintIndex++;
                _nextHintPoint = _config.TutorialHintData[_nextHintIndex].Point;
            }
            else
            {
                IsTutorialCleared = true;
                TutorialCleared?.Invoke(this, EventArgs.Empty);
                Disable();
            }
        }

        private void Update()
        {
            if (!_isEnabled)
            {
                return;
            }

            if (_whitePlayerCharacter.transform.position.z >= _nextHintPoint)
            {
                // show hint
                _tutorialUI.ShowHint(_config.TutorialHintData[_nextHintIndex].Image);
                Time.timeScale = 0;
            }
        }
    }
}