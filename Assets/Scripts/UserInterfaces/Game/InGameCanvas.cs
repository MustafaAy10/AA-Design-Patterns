namespace ArrowProject.UserInterface
{
    using UnityEngine.UI;
    using TMPro;
    using UnityEngine;
    using ArrowProject.Component;

    public class InGameCanvas : BaseCanvas
    {
        [SerializeField] private TextMeshProUGUI coinContainer;

        public delegate void InGameCanvasDelegate();

        public event InGameCanvasDelegate OnPauseButtonClick;

        public override UIComponent.MenuName menuName => UIComponent.MenuName.IN_GAME;

        protected override void Init()
        {

        }

        public void RequestPause()
        {
            if (OnPauseButtonClick != null)
                OnPauseButtonClick();
        }

    }
}