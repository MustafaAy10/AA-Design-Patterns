using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ArrowProject.Component
{
    public class LevelGenerator
    {
        private const string levelDataPath = "Levels/Level";

        private static int levelIndex
        {
            get { return PlayerPrefs.GetInt("levelIndex"); }
            set { PlayerPrefs.SetInt("levelIndex", value); }
        }

        public LevelGenerator()
        {

        }

        public void IncreaseLevelIndex()
        {
            levelIndex++;
        }

        public LevelData GetLevelData()
        {
            var levelData = Resources.Load<LevelData>(levelDataPath + (levelIndex+1));
            if(levelData == null)
            {
                levelIndex--;
                levelData = Resources.Load<LevelData>(levelDataPath + (levelIndex+1));
            }
            levelData.SetLevelIndex(levelIndex+1);
            return levelData;
        }
    }
}
