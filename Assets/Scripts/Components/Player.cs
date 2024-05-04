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
        private int levelArrowCount; // Arrow  e�er RotatingBoarda �arpt���nda bir azalacak,
        private int playerArrowCount; // Ba�lang��ta hem levelArrowCount hem playerArrowCount ayn� de�ere sahip,
                                       // ama playerArrowCount Player her Arrow f�rlatt���nda azalacak.

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
            if (playerArrowCount > 0)  // B�ylelikle son ok havada iken yani levelcomplete olmam�� iken
                                        // yeni Arrow f�rlat�lmas�n� engelliyoruz. Player'un Texti playerArrowCountu
                                        // g�steriyor, o da s�f�r iken yeni Arrow atma, ��nk� son Arrow havada olabilir
                                        // oyun durmam�� olabilir. 
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