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
        private int levelArrowCount; // Arrow  e�er RotatingBoarda �arpt���nda bir azalacak,
        private int playerArrowCount; // Ba�lang��ta hem levelArrowCount hem playerArrowCount ayn� de�ere sahip,
                                      // ama playerArrowCount Player her Arrow f�rlatt���nda azalacak.
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
            if (playerArrowCount > 0)  // B�ylelikle son ok havada iken yani levelcomplete olmam�� iken
                                       // yeni Arrow f�rlat�lmas�n� engelliyoruz. Player'un Texti playerArrowCountu
                                       // g�steriyor, o da s�f�r iken yeni Arrow atma, ��nk� son Arrow havada olabilir
                                       // oyun durmam�� olabilir. 
            {
                Arrow arrow = arrowCollector.GetArrow();
                arrow.transform.position = transform.position;
                // Alltaki sat�r ile 2D'de Arrow'un y�n�n� rotatingCircle'a bakacak �ekilde ayarlad�k.
                // B�ylelikle Spawn Noktas� yani Player position neresi olursa olsun hep RotatingCircle do�ru hareket edecekler.
                // Burada arrow.transform.up y�n� ayarland� RotatingCircle'e bakacak �ekilde,
                // Arrow.CallUpdate'te de arrow.transform.up y�n�nde Translate etmeye devam edece�iz.
                arrow.transform.up = rotatingCircle.transform.position - arrow.transform.position; // transform.LookAt() 3D �al��t��� i�in yanl�� y�nlendiriyordu, 2D i�in en iyi ��z�m bu. 

                // Alttaki iki sat�r hata veriyor 3D'de �al��t�klar� i�in. Oyun 2D, ilgisiz sonu� veriyor.
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