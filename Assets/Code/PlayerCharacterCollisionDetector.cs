using System;
using UnityEngine;

namespace Avangardum.TwilightRun
{
    public class PlayerCharacterCollisionDetector : MonoBehaviour
    {
        public event EventHandler<PlayerCharacterCollisionArgs> ObstacleCollision;
        public event EventHandler<PlayerCharacterCollisionArgs> CoinCollision;

        private void OnTriggerEnter(Collider other)
        {
            var otherGameObject = other.gameObject;
            GameColor otherColor = GameColor.None;
            
            // process obstacle collision
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
                else
                {
                    otherColor = GameColor.Red;
                }
                ObstacleCollision?.Invoke(this, new PlayerCharacterCollisionArgs {OtherGameObject = otherGameObject, OtherColor = otherColor});
            }
            
            // process coin collision
            if (otherGameObject.CompareTag("WhiteCoin") || otherGameObject.CompareTag("BlackCoin"))
            {
                if (otherGameObject.CompareTag("WhiteCoin"))
                {
                    otherColor = GameColor.White;
                }
                else
                {
                    otherColor = GameColor.Black;
                }
                CoinCollision?.Invoke(this, new PlayerCharacterCollisionArgs {OtherGameObject = otherGameObject, OtherColor = otherColor});
            }
        }
    }
}