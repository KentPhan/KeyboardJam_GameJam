using System;
using UnityEngine;

namespace Assets.Source.Events
{
    public abstract class ActionEntity : MonoBehaviour
    {
        [SerializeField] protected string m_Word;


        public virtual void Start()
        {
        }

        void Update()
        {
            //Debug.Log("Testing");
            if (!String.IsNullOrWhiteSpace(m_Word))
            {
                if (WordEventManager.Instance.HasInputedWord(m_Word))
                {
                    Toggle();
                    WordEventManager.Instance.BufferReset();
                }
            }

        }

        public abstract void Toggle();
    }
}
