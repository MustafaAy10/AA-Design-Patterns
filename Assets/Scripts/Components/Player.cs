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
        // private const float fireRate = 0.20f;
        // public float fireNextSpawn = 0f;

        // private float fireTime;
        private ArrowCollector arrowCollector;
        private int levelArrowCount; // Arrow  eðer RotatingBoarda çarptýðýnda bir azalacak,
        private int playerArrowCount; // Baþlangýçta hem levelArrowCount hem playerArrowCount ayný deðere sahip,
                                      // ama playerArrowCount Player her Arrow fýrlattýðýnda azalacak.
        private RotatingCircle rotatingCircle;

        private Vector2 startPosition;
        private Vector2 endPosition;
        [SerializeField] private float playerSpeed = 1;

        public void Initialize(ComponentContainer _componentContainer)
        {
            componentContainer = _componentContainer;
            gamePlayComponent = componentContainer.GetComponent(ComponentKeys.GamePlayComponent) as GamePlayComponent;
            if (playerTransform == null)
            {
                playerTransform = transform;
            }

            startPosition = playerTransform.position + Vector3.left * 1.5f;
            endPosition = playerTransform.position + Vector3.right * 1.5f;
        }

        public void Init()
        {
            Debug.Log("Player.Init() called...");
            //fireTime = 0;
            //fireNextSpawn = 0;
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
            var t = Mathf.PingPong(Time.time * playerSpeed, 1);
            playerTransform.position = Vector2.Lerp(startPosition, endPosition, t);
        }

        public void Inject(InGameInputSystem inputSystem, RotatingCircle rotatingCircle)
        {
            if (inputSystemReferance == null)
            {
                inputSystemReferance = inputSystem;
            }

            if (this.rotatingCircle == null)
            {
                this.rotatingCircle = rotatingCircle;
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
                // Alltaki satýr ile 2D'de Arrow'un yönünü rotatingCircle'a bakacak þekilde ayarladýk.
                // Böylelikle Spawn Noktasý yani Player position neresi olursa olsun hep RotatingCircle doðru hareket edecekler.
                // Burada arrow.transform.up yönü ayarlandý RotatingCircle'e bakacak þekilde,
                // Arrow.CallUpdate'te de arrow.transform.up yönünde Translate etmeye devam edeceðiz.
                arrow.transform.up = rotatingCircle.transform.position - arrow.transform.position; // transform.LookAt() 3D çalýþtýðý için yanlýþ yönlendiriyordu, 2D için en iyi çözüm bu. 

                // Alttaki iki satýr hata veriyor 3D'de çalýþtýklarý için. Oyun 2D, ilgisiz sonuç veriyor.
                // arrow.transform.rotation = Quaternion.LookRotation(rotatingCircle.transform.position - arrow.transform.position, Vector3.forward);
                // arrow.transform.LookAt(rotatingCircle.transform.position, Vector3.forward);

                arrow.SetArrowText(playerArrowCount);
                playerArrowCount--;
                UpdateText();
                // fireNextSpawn = fireTime;
            }
            //}
        }





    }
}