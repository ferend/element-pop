using System.Collections;
using System.Collections.Generic;
using Game.Controllers;
using Game.UI;
using UnityEngine;


namespace  Game.Managers
{
    public class UIManager : Manager
    { 
                   
        public enum PanelType
        {
            unset,
            gameHUD,
            lose
        }
        
        private PanelType currentPanel = PanelType.unset;

        [Header("Panels")]
        public UIScreen losePanel;
        public UIScreen gameHUD;
        
        
        public async void Setup()
        {
            SwitchPanel(PanelType.gameHUD);
        }
        
        public void SwitchPanel(PanelType type)
        {
            if(currentPanel != PanelType.unset)
                FetchPanel(currentPanel).CloseScreen();

            FetchPanel(type).OpenScreen();

            currentPanel = type;
        }
        
        private UIScreen FetchPanel(PanelType panelType)
        {
            switch (panelType)
            {
                case PanelType.gameHUD:
                    return gameHUD;
                case  PanelType.lose:
                    return losePanel;
                default:
                    Debug.LogWarning("ERROR: Could not fetch panel for some reason. Please check your code logic.");
                    return null;
            }
        }
        
    }

}
