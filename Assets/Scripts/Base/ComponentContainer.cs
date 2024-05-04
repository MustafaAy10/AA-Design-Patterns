namespace DevelopmentKit.Base.Component 
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ComponentContainer
    {
        private Dictionary<string, object> components;

        public ComponentContainer() 
        {
            components = new Dictionary<string, object>();
        }

        public void AddComponent(string componentKey, object component) 
        {
            if (components.ContainsKey(componentKey))
            {
                Debug.LogError("[ComponentContainer] Can't AddComponent because the key: " +  componentKey  + " is already exist.");
                return;
            }
            components.Add(componentKey, component);
        }

        public object GetComponent(string componentKey) 
        {
            if (!components.ContainsKey(componentKey)) 
            {
                return null;
            }

            return components[componentKey];
        }
    }

}

