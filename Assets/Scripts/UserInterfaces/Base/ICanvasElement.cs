using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArrowProject.UserInterface 
{
    public interface ICanvasElement
    {
        void Init();
        void Activate();
        void Deactivate();
    }
}


