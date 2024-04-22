namespace ArrowProject.Component
{
    using System.Collections.Generic;
    using DevelopmentKit.Base.Component;
    using DevelopmentKit.Base.Object;
    using DevelopmentKit.Base.Pattern.ObjectPool;

    public class ArrowCollector : IArrowCollector, IDestructible
    {
        private Pool<Arrow> pool;
        private const string SOURCE_OBJECT_PATH = "Prefabs/Arrow";
        private List<Arrow> activeArrows = new List<Arrow>();
        private List<Arrow> arrowsOnRotatingCircle = new List<Arrow>();

        private GamePlayComponent gamePlayComponent;

        public delegate void ArrowHitDelegate();
        public event ArrowHitDelegate OnArrowHitBoard;
        public event ArrowHitDelegate OnGameOver;

        public ArrowCollector(ComponentContainer componentContainer)
        {

            pool = new Pool<Arrow>(SOURCE_OBJECT_PATH);
            pool.PopulatePool(10);

            // gamePlayComponent = componentContainer.GetComponent(ComponentKeys.GamePlayComponent) as GamePlayComponent;
        }

        public Arrow GetBullet()
        {
            var arrow = pool.GetObjectFromPool();
            arrow.InjectArrowCollector(this);
            if (!activeArrows.Contains(arrow))
                activeArrows.Add(arrow);

            return arrow;
        }

        public void AddArrowToRotatingCircle(Arrow arrow)
        {
            activeArrows.Remove(arrow);
            arrowsOnRotatingCircle.Add(arrow);
        }

        public void AddArrowToPool(Arrow arrow)
        {
            arrow.SetDefault();
            pool.AddObjectToPool(arrow);
        }

        public void UpdateArrows()
        {
            foreach (var activeArrow in activeArrows)
            {
                if (activeArrow.isActiveAndEnabled)
                    activeArrow.CallUpdate();
            }
        }

        public void TriggerGameOver()
        {
            if (OnGameOver != null)
            {
                OnGameOver();
            }
        }

        public void TriggerArrowHitBoard()
        {
            if(OnArrowHitBoard != null)
            {
                OnArrowHitBoard();
            }
        }

        public void OnDestruct()
        {
            for (int i = 0; i < activeArrows.Count; i++)
            {
                AddArrowToPool(activeArrows[i]);
            }

            for (int i = 0; i < arrowsOnRotatingCircle.Count; i++)
            {
                AddArrowToPool(arrowsOnRotatingCircle[i]);
            }

            activeArrows.Clear();
            arrowsOnRotatingCircle.Clear();
        }
    }
}