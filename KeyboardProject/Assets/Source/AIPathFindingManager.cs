using UnityEngine;

namespace Assets.Source
{
    public class AIPathFindingManager : MonoBehaviour
    {
        public static AIPathFindingManager Instance;


        private AstarPath m_PathFinder;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            m_PathFinder = GetComponent<AstarPath>();
        }


        public void RescanGraph()
        {
            m_PathFinder.Scan();
        }
    }
}
