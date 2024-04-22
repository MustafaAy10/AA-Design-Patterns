using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ArrowProject.Component
{
    [CreateAssetMenu ( fileName ="LevelData" , menuName = "MoonLevels/LevelData", order = 1) ]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _arrowCount;
        [SerializeField] private float _rotatingCircleSpeed;
        private int _levelIndex = 0;

        public int arrowCount => _arrowCount;
        public float rotatingCircleSpeed => _rotatingCircleSpeed;
        public int levelIndex => _levelIndex;

        public void SetLevelIndex(int index)
        {
            _levelIndex = index;
        }
    }
}
