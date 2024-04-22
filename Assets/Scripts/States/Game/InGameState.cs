using ArrowProject.Component;
using DevelopmentKit.Base.Component;
using DevelopmentKit.HSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrowProject.State
{
    public class InGameState : StateMachine
    {
        private UIComponent uiComponent;
        private GamePlayComponent gamePlayComponent;


        public InGameState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent(ComponentKeys.UIComponent) as UIComponent;
            gamePlayComponent = componentContainer.GetComponent(ComponentKeys.GamePlayComponent) as GamePlayComponent;
        }

        protected override void OnEnter()
        {
            UnityEngine.Debug.Log("InGameState.OnEnter() called...");
            gamePlayComponent.OnEnter();

            gamePlayComponent.OnGameOver += OnGameOver;
            gamePlayComponent.OnLevelCompleted += OnLevelCompleted;
        }

        protected override void OnExit()
        {
            UnityEngine.Debug.Log("InGameState.OnExit() called...");

            gamePlayComponent.OnExit();
            
            gamePlayComponent.OnGameOver -= OnGameOver;
            gamePlayComponent.OnLevelCompleted -= OnLevelCompleted;
        }

        protected override void OnUpdate()
        {
            gamePlayComponent.CallUpdate();
        }

        private void OnGameOver()
        {
            SendTrigger((int)StateTriggers.END_GAME_REQUEST);
        }

        private void OnLevelCompleted()
        {
            SendTrigger((int)StateTriggers.NEXT_LEVEL_GAME_REQUEST);
        }
    }
}