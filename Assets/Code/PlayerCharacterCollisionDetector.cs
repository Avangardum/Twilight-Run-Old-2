using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class PlayerCharacterCollisionDetector : MonoBehaviour
    {
        public event EventHandler<PlayerCharacterCollisionArgs> ObstacleCollision;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("ඞ");
            var otherGameObject = other.gameObject;
            GameColor otherColor = GameColor.None;
            if (otherGameObject.CompareTag("WhiteObstacle") || otherGameObject.CompareTag("BlackObstacle") || otherGameObject.CompareTag("RedObstacle"))
            {
                if (otherGameObject.CompareTag("WhiteObstacle"))
                {
                    otherColor = GameColor.White;
                }
                else if (otherGameObject.CompareTag("BlackObstacle"))
                {
                    otherColor = GameColor.Black;
                }
                else if (otherGameObject.CompareTag("RedObstacle"))
                {
                    otherColor = GameColor.Red;
                }
                ObstacleCollision?.Invoke(this, new PlayerCharacterCollisionArgs {OtherGameObject = otherGameObject, OtherColor = otherColor});
            }
        }
    }
}