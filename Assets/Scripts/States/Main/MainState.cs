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
    public class MainState : StateMachine
    {
        private UIComponent uiComponent;
        private MainMenuState mainMenuState;

        public MainState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent(ComponentKeys.UIComponent) as UIComponent;

            mainMenuState = new MainMenuState(componentContainer);

            AddSubState(mainMenuState);
        }

        protected override void OnEnter()
        {
            UnityEngine.Debug.Log("MainState.OnEnter() called...");

        }

        protected override void OnExit()
        {
            UnityEngine.Debug.Log("MainState.OnExit() called...");

            SetDefaultState();
        }

        protected override void OnUpdate()
        {
            
        }
    }
}
