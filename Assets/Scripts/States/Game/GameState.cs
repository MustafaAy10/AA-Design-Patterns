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
    public class GameState : StateMachine
    {
        private UIComponent uiComponent;
        
        private InGameState inGameState;
        private EndGameState endGameState;
        private NextLevelState nextLevelState;

        public GameState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent(ComponentKeys.UIComponent) as UIComponent;
            
            inGameState = new InGameState(componentContainer);
            endGameState = new EndGameState(componentContainer);
            nextLevelState = new NextLevelState(componentContainer);

            AddSubState(inGameState);
            AddSubState(nextLevelState);    
            AddSubState(endGameState);

            AddTransition(inGameState, endGameState, (int)StateTriggers.END_GAME_REQUEST);
            AddTransition(inGameState, nextLevelState, (int)StateTriggers.NEXT_LEVEL_GAME_REQUEST);
            AddTransition(endGameState, inGameState, (int)StateTriggers.PLAY_INGAME_REQUEST);
            AddTransition(nextLevelState, inGameState, (int)StateTriggers.PLAY_INGAME_REQUEST);
            

        }

        protected override void OnEnter()
        {
            UnityEngine.Debug.Log("GameState.OnEnter() called...");

        }

        protected override void OnExit()
        {
            UnityEngine.Debug.Log("GameState.OnExit() called...");

            SetDefaultState(); // GameState'ten tekrar MainState'e döndüğünde SetDefaultState yapmalıyız, çünkü currentState defaultta kalmamış olabilir MainState'ten GameState'e geçişte.
        }

        protected override void OnUpdate()
        {

        }
    }
}
