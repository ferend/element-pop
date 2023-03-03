using System;
using Game.Controllers;
using Game.Entity;
using UnityEngine;

namespace Game.Managers
{
    public class GameFlowManager : System, IFlowManager
    {
        public event Action OnGameOver;
        public event Action OnGameStart;
        public event Action OnGameRestart;

        private InputManager _inputManager;
        private PlayerController _playerController;
        private BubbleController _bubbleController;
        private bool _canPlay;
        
        public static IFlowManager Manager { get; }

        
        protected override void SetupManagers()
        {
            base.SetupManagers();
            _inputManager = GetManager<InputManager>();
            _playerController = GetManager<PlayerController>();
            _bubbleController = GetManager<BubbleController>();
        }
        
        public override void Setup()
        {
            base.Setup();
            Deadline.OnBubbleCollision += GameOver;
        }
        public void GameStart()
        {
            
        }
        public void GameOver()
        {
            OnGameOver?.Invoke();
            _bubbleController.ResetGrid();
            Debug.Log("gameover");
        }
        
        public void SetPaused(bool isPaused)
        {
        }
    }
}
