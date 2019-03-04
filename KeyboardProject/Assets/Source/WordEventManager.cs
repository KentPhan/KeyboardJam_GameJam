using Assets.Source.Events;
using UnityEditor;
using UnityEngine;

namespace Assets.Source
{

    public class WordEventManager : MonoBehaviour
    {
        //[SerializeField] private ActionEntity[] m_Actions;
        private string m_CurrentInput;
        private bool m_BufferReset = false;


        public static WordEventManager Instance;

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
        }

        public void Start()
        {
            m_CurrentInput = string.Empty;
            m_BufferReset = false;
        }


        public void Update()
        {
            m_CurrentInput += Input.inputString;
        }

        public void LateUpdate()
        {
            if (m_BufferReset)
            {
                m_CurrentInput = string.Empty;
                m_BufferReset = false;
                AIPathFindingManager.Instance.RescanGraph();
            }
        }

        public bool HasInputedWord(string m_Word)
        {
            return m_CurrentInput.Contains(m_Word);
        }

        public void BufferReset()
        {
            m_BufferReset = true;
        }

    }
}
