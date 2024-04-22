namespace ArrowProject.UserInterface
{
    using UnityEngine.UI;
    using TMPro;
    using UnityEngine;
    using ArrowProject.Component;

    public class EndGameCanvas : BaseCanvas
    {
        public delegate void EndGameCanvasDelegate();

        public event EndGameCanvasDelegate OnRestartButtonClick;
        public event EndGameCanvasDelegate OnMainMenuButtonClick;

        public override UIComponent.MenuName menuName => UIComponent.MenuName.END_GAME;

        protected override void Init()
        {
        }

        public void RequestRestart()
        {
            if (OnRestartButtonClick != null)
                OnRestartButtonClick();
        }
        public void RequestMainMenu()
        {
            if (OnMainMenuButtonClick != null)
                OnMainMenuButtonClick();
        }
        
    }
}