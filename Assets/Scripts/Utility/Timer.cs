using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArrowProject.Helper
{
    public class Timer : MonoBehaviour
    {
        public void Time(Action action, float waitTime)
        {
            StartCoroutine(Action(action, waitTime));
        }

        IEnumerator Action(Action action, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            action?.Invoke();
        }
    }
}
