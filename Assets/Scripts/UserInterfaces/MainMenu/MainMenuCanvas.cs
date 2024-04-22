using ArrowProject.Component;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ArrowProject.UserInterface
{
    public class MainMenuCanvas : BaseCanvas
    {
        public delegate void MainMenuRequestDelegate();

        public event MainMenuRequestDelegate OnPlayButtonClick;
        
        public override UIComponent.MenuName menuName => UIComponent.MenuName.MAIN_MENU;

        protected override void Init()
        {
            
        }

        public void RequestPlayGame()
        {
            if (OnPlayButtonClick != null)
                OnPlayButtonClick();
        }

    }
}