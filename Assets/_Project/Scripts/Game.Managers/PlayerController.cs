using System;
using Game.Entity;
using UnityEngine;

namespace Game.Managers
{
    public class PlayerController : Manager
    {
        public Action OnPlayerFail;
        private Player _player;
        public override void Setup()
        {
            _player = GameObject.FindObjectOfType<Player>();
            OnPlayerFail += ExampleFail;
        }

        private void ExampleFail()
        {
            Debug.Log("player failed");
        }
        
    }
}
