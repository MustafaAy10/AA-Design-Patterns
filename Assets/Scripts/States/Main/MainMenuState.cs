using ArrowProject.Component;
using ArrowProject.UserInterface;
using DevelopmentKit.Base.Component;
using DevelopmentKit.HSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrowProject.State
{
    public class MainMenuState : StateMachine
    {
        private UIComponent uiComponent;
        private MainMenuCanvas mainMenuCanvas;
        private GamePlayComponent gamePlayComponent;

        public MainMenuState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent(ComponentKeys.UIComponent) as UIComponent;
            gamePlayComponent = componentContainer.GetComponent(ComponentKeys.GamePlayComponent) as GamePlayComponent;
            mainMenuCanvas = uiComponent.GetCanvas(UIComponent.MenuName.MAIN_MENU) as MainMenuCanvas;
        }

        private void OnPlayButtonClick()
        {
            UnityEngine.PlayerPrefs.SetInt("levelIndex", 0);
            SendTrigger((int)StateTriggers.START_GAME_REQUEST);
        }

        protected override void OnEnter()
        {
            UnityEngine.Debug.Log("MainMenuState.OnEnter() called...");

            mainMenuCanvas.OnPlayButtonClick += OnPlayButtonClick;
            uiComponent.EnableCanvas(UIComponent.MenuName.MAIN_MENU);
        }

        protected override void OnExit()
        {
            UnityEngine.Debug.Log("MainMenuState.OnExit() called...");

            mainMenuCanvas.OnPlayButtonClick -= OnPlayButtonClick;
            uiComponent.CloseCanvas();

        }

        protected override void OnUpdate()
        {

        }



    }
}
