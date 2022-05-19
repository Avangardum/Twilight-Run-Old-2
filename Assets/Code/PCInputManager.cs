using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    class PCInputManager : MonoBehaviour, IInputManager
    {
        public event EventHandler Tap;

        private void Update()
        {
            if (Input.anyKey)
            {
                Tap?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}