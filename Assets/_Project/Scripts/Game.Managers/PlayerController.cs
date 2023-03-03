using System;
using Game.Entity;
using UnityEngine;

namespace Game.Managers
{
    public class PlayerController : Manager
    {
        private Player _player;
        public override void Setup()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

    }
}
