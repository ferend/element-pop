using System;
using UnityEngine;

namespace Game.Entity
{
    public class Deadline : MonoBehaviour
    {
        public static event Action OnBubbleCollision;
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Ball"))
            {
                OnBubbleCollision?.Invoke();
            }
        }
    }
}
