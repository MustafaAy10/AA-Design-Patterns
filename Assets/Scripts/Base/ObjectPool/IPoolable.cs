using System.Collections;
using System.Collections.Generic;
using ArrowProject.Component;
using UnityEngine;

namespace DevelopmentKit.Base.Pattern.ObjectPool 
{
    public interface IPoolable
    {
        void Activate();
        void Deactivate();
        void Initialize();
        void InjectArrowCollector(ArrowCollector arrowCollector);

    }

}

