using UnityEngine;

namespace Assets.Source.Events
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(MeshRenderer))]
    public class DoorEntity : ActionEntity
    {
        private MeshRenderer m_Mesh;
        private Collider2D m_Collider;

        // Start is called before the first frame update
        void Start()
        {
            m_Mesh = GetComponent<MeshRenderer>();
            m_Collider = GetComponent<BoxCollider2D>();

            base.Start();
        }



        public override void Toggle()
        {
            m_Mesh.enabled = !m_Mesh.enabled;
            m_Collider.enabled = !m_Collider.enabled;
        }
    }
}
