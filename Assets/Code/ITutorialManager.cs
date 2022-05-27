using System;

namespace Avangardum.TwilightRun
{
    public interface ITutorialManager
    {
        bool IsTutorialCleared { get; }
        event EventHandler TutorialCleared;
        void Disable();
        void Enable();
        void Initialize(ITutorialConfig tutorialConfig, ITutorialUI tutorialUI);
    }
}