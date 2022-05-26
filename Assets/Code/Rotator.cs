using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationSpeed;

        private void FixedUpdate()
        {
            transform.Rotate(_rotationSpeed * Time.fixedDeltaTime);
        }
    }
}