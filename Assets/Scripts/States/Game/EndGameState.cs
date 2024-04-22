using ArrowProject.Component;
using ArrowProject.UserInterface;
using DevelopmentKit.Base.Component;
using DevelopmentKit.HSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ArrowProject.State
{
    public class EndGameState : StateMachine
    {
        private UIComponent uiComponent;
        private GamePlayComponent gamePlayComponent;
        private EndGameCanvas endGameCanvas;
        private Animator mainCameraAnimator;

        private float timer = 0;
        private const float waitTime = 2;
        private bool isActivated = false;

        public EndGameState(ComponentContainer componentContainer)
        {

            uiComponent = componentContainer.GetComponent(ComponentKeys.UIComponent) as UIComponent;
            endGameCanvas = uiComponent.GetCanvas(UIComponent.MenuName.END_GAME) as EndGameCanvas;

            gamePlayComponent = componentContainer.GetComponent(ComponentKeys.GamePlayComponent) as GamePlayComponent;

            // 1.Yöntem
            // mainCameraAnimator = Camera.main.GetComponent<Animator>();
            
            // 2.Yöntem
            mainCameraAnimator = (componentContainer.GetComponent(ComponentKeys.GameCameraComponent) as GameCameraComponent).animator;

        }

        protected override void OnEnter()
        {
            UnityEngine.Debug.Log("EndGameState.OnEnter() called...");

            timer = 0;
            isActivated = false;

            mainCameraAnimator.SetTrigger("GameOver");
        }

        private void ActivateEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.END_GAME);
            endGameCanvas.OnMainMenuButtonClick += OnMainMenuRequest;
            endGameCanvas.OnRestartButtonClick += OnRestartRequest;
        }

        protected override void OnExit()
        {
            UnityEngine.Debug.Log("EndGameState.OnExit() called...");

            uiComponent.CloseCanvas();
            endGameCanvas.OnMainMenuButtonClick -= OnMainMenuRequest;
            endGameCanvas.OnRestartButtonClick -= OnRestartRequest;
            
            mainCameraAnimator.SetTrigger("GamePlay");

        }

        protected override void OnUpdate()
        {
            if (!isActivated)
            {
                timer += Time.deltaTime;
                if(timer > waitTime)
                {
                    ActivateEnter();
                    isActivated = true;
                }
            }
        }

        private void OnMainMenuRequest()
        {
            SendTrigger((int)StateTriggers.RETURN_MAIN_MENU);
        }

        private void OnRestartRequest()
        {
            SendTrigger((int)StateTriggers.PLAY_INGAME_REQUEST);
        }
    }
}