namespace ArrowProject.Component
{
    using UnityEngine;
    using DevelopmentKit.Base.Object;
    using DevelopmentKit.Base.Component;
    using TMPro;

    public class Player : MonoBehaviour, IUpdatable, IInitializable, IDestructible
    {
        private InGameInputSystem inputSystemReferance;
        private Transform playerTransform;

        [SerializeField] private TextMeshProUGUI levelArrowCountText;

        private ComponentContainer componentContainer;
        private GamePlayComponent gamePlayComponent;

        private bool isGameOver = false;
        private const float fireRate = 0.20f;
        public float fireNextSpawn = 0f;

        private float fireTime;
        private ArrowCollector arrowCollector;
        private int levelArrowCount; // Arrow  eðer RotatingBoarda çarptýðýnda bir azalacak,
        private int playerArrowCount; // Baþlangýçta hem levelArrowCount hem playerArrowCount ayný deðere sahip,
                                       // ama playerArrowCount Player her Arrow fýrlattýðýnda azalacak.

        public void Initialize(ComponentContainer _componentContainer)
        {
            componentContainer = _componentContainer;
            gamePlayComponent = componentContainer.GetComponent(ComponentKeys.GamePlayComponent) as GamePlayComponent;
            if (playerTransform == null)
            {
                playerTransform = transform;
            }
        }

        public void Init()
        {
            Debug.Log("Player.Init() called...");
            fireTime = 0;
            fireNextSpawn = 0;
            isGameOver = false;
            inputSystemReferance.OnScreenTouch += OnScreenTouch;
            arrowCollector.OnArrowHitBoard += OnArrowHitBoard;
        }

        public void InjectArrowCollector(ArrowCollector arrowCollector)
        {
            this.arrowCollector = arrowCollector;
        }

        public void CallUpdate()
        {
            fireTime += Time.deltaTime;
        }

        public void InjectInputSystem(InGameInputSystem inputSystem)
        {
            if (inputSystemReferance == null)
            {
                inputSystemReferance = inputSystem;
            }
        }

        private void OnScreenTouch()
        {
            Shoot();
        }

        public void OnDestruct()
        {
            inputSystemReferance.OnScreenTouch -= OnScreenTouch;
            arrowCollector.OnArrowHitBoard -= OnArrowHitBoard;
        }

        public void SetLevel(int count)
        {
            levelArrowCount = count;
            playerArrowCount = count;
            UpdateText();
        }

        private void UpdateText()
        {
            levelArrowCountText.text = playerArrowCount.ToString();
        }

        private void OnArrowHitBoard()
        {
            levelArrowCount--;

            if (levelArrowCount == 0)
            {
                gamePlayComponent.TriggerLevelCompleted();
            }

        }

        private void Shoot()
        {
            //if (fireTime > fireNextSpawn + fireRate)
            //{
            if (playerArrowCount > 0)  // Böylelikle son ok havada iken yani levelcomplete olmamýþ iken
                                        // yeni Arrow fýrlatýlmasýný engelliyoruz. Player'un Texti playerArrowCountu
                                        // gösteriyor, o da sýfýr iken yeni Arrow atma, çünkü son Arrow havada olabilir
                                        // oyun durmamýþ olabilir. 
            {
                Arrow arrow = arrowCollector.GetArrow();
                arrow.transform.position = transform.position;

                arrow.SetArrowText(playerArrowCount);
                playerArrowCount--;
                UpdateText();
                // fireNextSpawn = fireTime;
            }
            //}
        }





    }
}