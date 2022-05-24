using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class PlayerCharacterCollisionArgs : EventArgs
    {
        public GameObject OtherGameObject;
        public GameColor OtherColor;
    }
}