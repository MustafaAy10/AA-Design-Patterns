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
    public class NextLevelState : StateMachine
    {
        private UIComponent uiComponent;
        private NextLevelCanvas nextLevelCanvas;
        private GamePlayComponent gamePlayComponent;
        private Animator mainCameraAnimator;

        private float timer = 0;
        private const float waitTime = 2;
        private bool isActivated = false;

        public NextLevelState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent(ComponentKeys.UIComponent) as UIComponent;
            gamePlayComponent = componentContainer.GetComponent(ComponentKeys.GamePlayComponent) as GamePlayComponent;
            nextLevelCanvas = uiComponent.GetCanvas(UIComponent.MenuName.NEXT_LEVEL_GAME) as NextLevelCanvas;

            // 1.Yöntem
            // mainCameraAnimator = Camera.main.GetComponent<Animator>();

            // 2.Yöntem
            mainCameraAnimator = (componentContainer.GetComponent(ComponentKeys.GameCameraComponent) as GameCameraComponent).animator;
        }

        protected override void OnEnter()
        {
            UnityEngine.Debug.Log("NextLevelState.OnEnter() called...");

            timer = 0;
            isActivated = false;
            mainCameraAnimator.SetTrigger("NextLevel");
        }

        private void ActivateEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.NEXT_LEVEL_GAME);
            nextLevelCanvas.OnMainMenuButtonClick += OnMainMenuRequest;
            nextLevelCanvas.OnNextLevelButtonClick += OnNextLevelRequest;
        }

        protected override void OnExit()
        {
            UnityEngine.Debug.Log("NextLevelState.OnExit() called...");

            uiComponent.CloseCanvas();
            nextLevelCanvas.OnMainMenuButtonClick -= OnMainMenuRequest;
            nextLevelCanvas.OnNextLevelButtonClick -= OnNextLevelRequest;

            mainCameraAnimator.SetTrigger("GamePlay");
        }

        protected override void OnUpdate()
        {
            if (!isActivated)
            {
                timer += Time.deltaTime;
                if (timer > waitTime)
                {
                    ActivateEnter();
                    isActivated = true;
                }
            }
        }

        private void OnNextLevelRequest()
        {
            SendTrigger((int)StateTriggers.PLAY_INGAME_REQUEST);
        }

        private void OnMainMenuRequest()
        {
            SendTrigger((int)StateTriggers.RETURN_MAIN_MENU);
        }
    }
}