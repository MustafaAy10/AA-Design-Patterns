namespace ArrowProject.Component
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DevelopmentKit.Base.Component;
    using DevelopmentKit.Base.Object;

    public class InGameInputSystem : IUpdatable, IComponent, IDestructible, IInitializable
    {
        public delegate void TouchMessageDelegate();
        public event TouchMessageDelegate OnScreenTouch; 

        private bool isInputAvailable = false;

        public void CallUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Debug.Log("OnScreenTouch");
                if (OnScreenTouch != null) 
                {
                    OnScreenTouch();
                }
            }
        }

        public void CallFixedUpdate()
        {
            
        }

        public void CallLateUpdate()
        {
            
        }

        public void Initialize(ComponentContainer componentContainer)
        {
            
        }

        public void OnDestruct()
        {
            //TODO: finish input system
            isInputAvailable = false;
        }

        public void Init()
        {
            //TODO: initialize the system
            isInputAvailable = true;
        }
    }
}