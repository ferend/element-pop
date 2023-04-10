using System;
using System.Collections;
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
        private BubbleController _bubbleController;
        private UIManager _uiManager;
        
        protected override void SetupManagers()
        {
            base.SetupManagers();
            _inputManager = GetManager<InputManager>();
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
            StartCoroutine(SetCanShoot());
        }
        public void GameOver()
        {
            OnGameOver?.Invoke();
            _uiManager.SwitchPanel(UIManager.PanelType.lose);
            _inputManager.BaseInput.GetComponent<PlayerInput>()._canShoot = false;
        }

        private IEnumerator SetCanShoot()
        {
            yield return new WaitForSeconds(1.5f);
            _inputManager.BaseInput.GetComponent<PlayerInput>()._canShoot = true;
        }
    }
}
