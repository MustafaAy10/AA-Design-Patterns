namespace ArrowProject.Component
{
    using DevelopmentKit.Base.Component;
    using DevelopmentKit.Base.Pattern.ObjectPool;
    using DevelopmentKit.Base.Object;
    using UnityEngine;
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class GamePlayComponent : MonoBehaviour, IComponent, IUpdatable
    {
        public delegate void GameOverDelegate();
        public delegate void LevelCompletedDelegate();

        public event GameOverDelegate OnGameOver;
        public event LevelCompletedDelegate OnLevelCompleted;

        private Player player;
        // [SerializeField] private GameObject playerPrefab;
        // [SerializeField] private GameCamera gameCamera;
        private InGameInputSystem inputSystem;

        private ArrowCollector arrowCollector;
        private ComponentContainer componentContainer;
        private RotatingCircle rotatingCircle;
        private LevelGenerator levelGenerator;

        private bool isGameOver;

        public void Initialize(ComponentContainer componentContainer)
        {
            this.componentContainer = componentContainer;

            inputSystem = componentContainer.GetComponent(ComponentKeys.InGameInputSystem) as InGameInputSystem;

            arrowCollector = new ArrowCollector(componentContainer);
            rotatingCircle = FindObjectOfType<RotatingCircle>();
            rotatingCircle.Initialize();
            levelGenerator = new LevelGenerator();

            CreatePlayer();

        }

        private void CreatePlayer()
        {
            player = FindObjectOfType<Player>();
            player.Initialize(componentContainer);
            player.InjectInputSystem(inputSystem);
            player.InjectArrowCollector(arrowCollector);
        }

        public void SendGameIsStartedMessage()
        {
            // isGameOver = false;
        }

        public void CallUpdate()
        {
            inputSystem.CallUpdate();
            rotatingCircle.CallUpdate();
            player.CallUpdate();
            arrowCollector.UpdateArrows();
        }

        public bool IsGameOver()
        {
            return isGameOver;
        }

        public void OnEnter()
        {
            Destruct();

            LoadLevel();

            inputSystem.Init();
            player.Init();
        }

        public void LoadLevel()
        {
            var levelData = levelGenerator.GetLevelData();
            player.SetLevel(levelData.arrowCount);
            rotatingCircle.SetLevel(levelData.levelIndex, levelData.rotatingCircleSpeed);
        }

        public void OnExit()
        {

        }

        public void TriggerGameOver()
        {
            if (OnGameOver != null)
            {
                OnGameOver();
            }
        }

        private void Destruct()
        {
            player.OnDestruct();
            inputSystem.OnDestruct();
            arrowCollector.OnDestruct();
        }

        public void TriggerLevelCompleted()
        {
            levelGenerator.IncreaseLevelIndex();
           
            if (OnLevelCompleted != null)
            {
                OnLevelCompleted();
            }

        }

        public Player Player => player;

    }

    public interface IArrowCollector
    {
        void AddArrowToPool(Arrow arrow);
        void AddArrowToRotatingCircle(Arrow arrow);
    }
}