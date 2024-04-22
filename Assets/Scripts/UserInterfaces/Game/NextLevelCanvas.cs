namespace ArrowProject.UserInterface
{
    using UnityEngine.UI;
    using TMPro;
    using UnityEngine;
    using ArrowProject.Component;

    public class NextLevelCanvas : BaseCanvas
    {
        public delegate void InGameCanvasDelegate();

        public event InGameCanvasDelegate OnNextLevelButtonClick;
        public event InGameCanvasDelegate OnMainMenuButtonClick;

        public override UIComponent.MenuName menuName => UIComponent.MenuName.NEXT_LEVEL_GAME;

        protected override void Init()
        {

        }

        public void RequestNextLevel()
        {
            if (OnNextLevelButtonClick != null)
                OnNextLevelButtonClick();
        }

        public void RequestMainMenu()
        {
            if (OnMainMenuButtonClick != null)
                OnMainMenuButtonClick();
        }

    }
}