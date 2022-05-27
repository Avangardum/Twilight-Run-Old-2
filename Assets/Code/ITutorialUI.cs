using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public interface ITutorialUI
    {
        event EventHandler NextPressed;
        void ShowHint(Sprite image);
    }
}