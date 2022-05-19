using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class MobileInputManager : MonoBehaviour, IInputManager
    {
        public event EventHandler Tap;
    }
}