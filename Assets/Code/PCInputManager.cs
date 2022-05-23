using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class PCInputManager : MonoBehaviour, IInputManager
    {
        public event EventHandler Tap;

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                Tap?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}