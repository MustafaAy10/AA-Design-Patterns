namespace ArrowProject.Component
{
    using DevelopmentKit.Base.Component;
    using ArrowProject.UserInterface;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class UIComponent : MonoBehaviour, IComponent
    {
        public enum MenuName
        {
             MAIN_MENU, IN_GAME,
             END_GAME, 
             NEXT_LEVEL_GAME
        }

        private BaseCanvas activeCanvas = null;

        // Mustafa Ay ekledi.
        private List<BaseCanvas> allCanvas;

        public void Initialize(ComponentContainer componentContainer)
        {
            // Yeni yöntem: Mustafa Ay.
            allCanvas = FindObjectsOfType<BaseCanvas>(true).ToList();
            allCanvas.ForEach(canvas => Debug.Log("[UIComponent].Initialize() allCanvas: " + canvas.menuName.ToString()));
            allCanvas.ForEach(x => x.Initialize(componentContainer));
            allCanvas.ForEach(x => DeactivateCanvas(x));

        }

        public BaseCanvas GetCanvas(MenuName canvas)
        {
            // Yeni Yöntem: Mustafa Ay.
            Debug.Log("[UIComponent].GetCanvas()  canvas: "+ canvas.ToString());
            return allCanvas.FirstOrDefault(x => x.menuName == canvas);

        }

        public void EnableCanvas(MenuName menuName)
        {
            DeactivateCanvas(activeCanvas);
            ActivateCanvas(menuName);
        }

        public void CloseCanvas()
        {
            DeactivateCanvas(activeCanvas);
        }

        private void DeactivateCanvas(BaseCanvas canvas)
        {
            if (canvas)
            {
                canvas.Deactivate();
            }
        }

        private void ActivateCanvas(MenuName menuName)
        {
            // Yeni Yöntem: Mustafa Ay.
            activeCanvas = allCanvas.FirstOrDefault(x => x.menuName == menuName);

            if (activeCanvas)
            {
                activeCanvas.Activate();
            }
        }
    }
}


