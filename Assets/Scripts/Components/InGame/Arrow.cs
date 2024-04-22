namespace ArrowProject.Component
{
    using DevelopmentKit.Base.Object;
    using DevelopmentKit.Base.Pattern.ObjectPool;
    using TMPro;
    using UnityEngine;
    using Random = UnityEngine.Random;

    // [RequireComponent(typeof(Rigidbody2D))]
    public class Arrow : MonoBehaviour, IPoolable, IUpdatable
    {
        private const float SPEED = 18f;
        private Transform _transform;
        private Rigidbody2D rigidbody;

        private Vector2 touchPointOnRotatingCicle = new Vector2(0, -0.73f);

        [SerializeField] private TextMeshProUGUI arrowText;

        public delegate void ArrowPoolDelegate(Arrow arrow);

        public ArrowPoolDelegate OnArrowOutOfScreen;

        private ArrowCollector arrowCollector;

        private bool _touched = false;

        public bool touched 
        {
            get { return _touched; }
            set { _touched = value; }
        }

        public void Initialize()
        {
            _transform = GetComponent<Transform>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetArrowText(int count)
        {
            arrowText.text = count.ToString();
        }

        public void CallUpdate()
        {
            _transform.Translate(Vector2.up * SPEED * Time.deltaTime, Space.World);
            // rigidbody.MovePosition(rigidbody.position + Vector2.up * SPEED * Time.deltaTime);
        }

        public void InjectArrowCollector(ArrowCollector _arrowCollector)
        {
            if (arrowCollector != null)
            {
                return;
            }

            arrowCollector = _arrowCollector;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void SetDefault()
        {
            _touched = false;
            transform.parent = null;
            transform.rotation = UnityEngine.Quaternion.identity;
        }

        public void OnTriggerEnter2D(Collider2D other)
        // private void OnCollisionEnter2D(Collision2D other)
        {
            // Debug.Log("Arrow.OnCollisionEnter2D() called...");
            Debug.Log("Arrow.OnTriggerEnter2D() called...");

            if (!_touched && other.gameObject.CompareTag("RotatingCircle"))
            {
                _transform.SetParent(other.transform);
                CorrectPosition();
                arrowCollector.AddArrowToRotatingCircle(this);
                _touched = true;
                arrowCollector.TriggerArrowHitBoard();
            }
            else if (!_touched && other.gameObject.CompareTag("Arrow"))
            {
                // Alltaki if'e gerek yok. Havada iki Arrow birbirine deðse de çalýþacak gameOver..
                //if (other.gameObject.GetComponent<Arrow>()._touched)
                //{
                _touched = true;
                other.GetComponent<Arrow>().touched = true; // Bu satýr ile havada çarpýþan iki Arrow durumunda veya
                                                            // biri havada diðeri RotatingBoard'ta olan iki Arrow çarpýþtýðýnda
                                                            // sadece bir Arrow scriptinin bu if içi çalýþacak diðeri çalýþmayacak
                                                            // böylelikle iki kez gameOver çalýþtýrmayacaðýz.
                arrowCollector.AddArrowToRotatingCircle(this);
                arrowCollector.TriggerGameOver();
                //}

            }

        }

        // Rotating Circle'a Arrow saplandýðýnda özellikle yüksek hýzlarda, dönen çemberin içine giriyor, onu çemberin yüzeyine taþýyoruz.
        private void CorrectPosition()
        {
            _transform.position = touchPointOnRotatingCicle;
        }

    }
}