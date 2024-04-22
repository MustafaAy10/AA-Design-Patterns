namespace ArrowProject
{
    using DevelopmentKit.Base.Component;
    using ArrowProject.Component;
    using ArrowProject.State;
    using System;
    using UnityEngine;

    public class MainComponent : MonoBehaviour
    {
        private ComponentContainer componentContainer;
        private UIComponent uIComponent;
        private GamePlayComponent gamePlayComponent;
        private InGameInputSystem inGameInputSystem;
        private GameCameraComponent gameCameraComponent;
        
        private AppState appState;

        private void Awake()
        {
            componentContainer = new ComponentContainer();
        }

        private void Start()
        {
            CreateUIComponent();
            CreateGamePlayComponent();
            CreateInGameInputSystem();
            CreateGameCameraComponent();

            InitializeComponents();
            CreateAppState();
            appState.Enter();
        }

        public void Update()
        {
            appState.Update();
        }

        private void CreateUIComponent()
        {
            uIComponent = FindObjectOfType<UIComponent>();
            //TODO: check is there any ui component object in the scene!!
            componentContainer.AddComponent(ComponentKeys.UIComponent, uIComponent);
        }

        private void CreateGamePlayComponent()
        {
            gamePlayComponent = FindObjectOfType<GamePlayComponent>();
            componentContainer.AddComponent(ComponentKeys.GamePlayComponent, gamePlayComponent);
        }  

        private void CreateInGameInputSystem()
        {
            inGameInputSystem = new InGameInputSystem();
            componentContainer.AddComponent(ComponentKeys.InGameInputSystem, inGameInputSystem);
        }

        private void CreateGameCameraComponent()
        {
            gameCameraComponent = FindObjectOfType<GameCameraComponent>();  
            componentContainer.AddComponent(ComponentKeys.GameCameraComponent, gameCameraComponent);
        }

        private void InitializeComponents()
        {
            uIComponent.Initialize(componentContainer);
            gamePlayComponent.Initialize(componentContainer);
            inGameInputSystem.Initialize(componentContainer);
            gameCameraComponent.Initialize(componentContainer);
        }

        private void CreateAppState()
        {
            appState = new AppState(componentContainer);
        }
    }
}