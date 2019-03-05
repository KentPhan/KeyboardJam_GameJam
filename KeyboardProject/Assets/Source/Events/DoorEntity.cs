using TMPro;
using UnityEngine;

namespace Assets.Source.Events
{
    public class DoorEntity : MonoBehaviour
    {
        private MeshRenderer m_Mesh;
        private Collider2D m_Collider;
        private TextMeshPro m_Text;


        private string m_Word = "NONE";
        private bool m_Active;

        private void Awake()
        {
            m_Mesh = GetComponentInChildren<MeshRenderer>();
            m_Collider = GetComponentInChildren<BoxCollider2D>();
            m_Text = GetComponentInChildren<TextMeshPro>();

            m_Text.text = m_Word;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void SetWord(string i_Word)
        {
            m_Word = i_Word;
            m_Text.text = m_Word;
        }

        public void Toggle()
        {
            m_Mesh.enabled = !m_Mesh.enabled;
            m_Collider.enabled = !m_Collider.enabled;
            m_Active = !m_Active;
        }

        public void Activate()
        {
            m_Mesh.enabled = true;
            m_Collider.enabled = true;
            m_Active = true;
        }

        public void DeActivate()
        {
            m_Mesh.enabled = false;
            m_Collider.enabled = false;
            m_Active = false;
        }
    }
}
