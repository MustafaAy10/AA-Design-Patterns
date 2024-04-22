
namespace ArrowProject.State 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DevelopmentKit.HSM;
    using DevelopmentKit.Base.Component;
    using ArrowProject.Component;

    public class AppState : StateMachine
    {
        
        private MainState mainState;
        private GameState gameState;

        private UIComponent uiComponent;

        public AppState(ComponentContainer componentContainer) 
        {
            uiComponent = componentContainer.GetComponent(ComponentKeys.UIComponent) as UIComponent;

            mainState = new MainState(componentContainer);
            gameState = new GameState(componentContainer);

            this.AddSubState(mainState);
            this.AddSubState(gameState);

            this.AddTransition(mainState, gameState, (int)StateTriggers.START_GAME_REQUEST);
            this.AddTransition(gameState, mainState, (int)StateTriggers.RETURN_MAIN_MENU);
        }

        protected override void OnEnter()
        {
            Debug.Log("AppState OnEnter");
        }

        protected override void OnExit()
        {
            Debug.Log("AppState OnExit");
        }

        protected override void OnUpdate()
        {
            
        }
    }

}


