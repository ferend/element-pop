using System;
using Game.Controllers;
using Game.Entity;
using Game.Helpers;
using UnityEngine;

namespace Game.Managers
{
    public class GameFlowManager : System
    {
        public event Action OnGameOver;
        public event Action OnGameStart;
        public event Action OnGameRestart;

        private InputManager _inputManager;
        private PlayerController _playerController;
        private BubbleController _bubbleController;
        private UIManager _uiManager;
        private bool _canPlay;
        
        protected override void SetupManagers()
        {
            base.SetupManagers();
            _inputManager = GetManager<InputManager>();
            _playerController = GetManager<PlayerController>();
            _bubbleController = GetManager<BubbleController>();
            _uiManager = GetManager<UIManager>();
        }
        
        public override void Setup()
        {
            base.Setup();
            Deadline.OnBubbleCollision += GameOver;

            _uiManager.Setup();
        }
        public void GameStart()
        {
            _uiManager.SwitchPanel(UIManager.PanelType.gameHUD);
            _bubbleController.ResetGrid();
            _bubbleController.ResetShootCount();
            _inputManager.BaseInput.GetComponent<PlayerInput>()._canShoot = true;
        }
        public void GameOver()
        {
            OnGameOver?.Invoke();
            _uiManager.SwitchPanel(UIManager.PanelType.lose);
            Debug.Log("gameover");
            _inputManager.BaseInput.GetComponent<PlayerInput>()._canShoot = false;
        }
        
        public void SetPaused(bool isPaused)
        {
        }
    }
}
