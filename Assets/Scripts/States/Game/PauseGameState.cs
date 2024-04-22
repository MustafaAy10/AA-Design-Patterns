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
    public class PauseGameState : StateMachine
    {

        private UIComponent uiComponent;

        public PauseGameState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent(ComponentKeys.UIComponent) as UIComponent;
        }

        protected override void OnEnter()
        {

        }

        protected override void OnExit()
        {

        }

        protected override void OnUpdate()
        {

        }
    }
}