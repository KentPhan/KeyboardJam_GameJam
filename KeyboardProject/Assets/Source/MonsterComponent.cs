using Pathfinding;
using UnityEngine;

namespace Assets.Source
{
    public class MonsterComponent : MonoBehaviour
    {
        private PlayerComponent m_Target;
        private AIPath m_AIPathComponent;

        // Start is called before the first frame update
        void Start()
        {
            m_AIPathComponent = GetComponent<AIPath>();

        }

        public void SetTarget(PlayerComponent i_Target)
        {
            m_Target = i_Target;
            m_AIPathComponent.destination = m_Target.transform.position;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
