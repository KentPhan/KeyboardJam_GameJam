using System;
using UnityEngine;

namespace Assets.Source.Events
{
    public abstract class ActionEntity : MonoBehaviour
    {
        protected string m_Word;

        protected bool m_Active = false;

        public virtual void Start()
        {
        }



        public abstract void Toggle();
        public abstract void Activate();
        public abstract void DeActivate();

        public string GetWord()
        {
            return m_Word;
        }



    }
}
