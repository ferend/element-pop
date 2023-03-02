using System;
using UnityEngine;

namespace Game.Managers
{
    public class GameFlowManager : System, IFlowManager
    {
        public event Action OnGameOver;
        public event Action OnGameStart;
        public event Action OnGameFinish;
        public event Action OnGameRestart;

        private InputManager _inputManager;
        private PlayerController _playerController;
        private bool _canPlay;
        
        public static IFlowManager Manager { get; }

        
        protected override void SetupManagers()
        {
            base.SetupManagers();
            _inputManager = GetManager<InputManager>();
            _playerController = GetManager<PlayerController>();
        }
        
        public override void Setup()
        {
            base.Setup();
            _playerController.OnPlayerFail += GameOver;

        }
        public void GameStart()
        {
            
        }
        public void GameOver()
        {
            OnGameOver?.Invoke();
            Debug.Log("gameover");
        }
        public void GameFinish()
        {
        }

        public void SetPaused(bool isPaused)
        {
        }
    }
}
