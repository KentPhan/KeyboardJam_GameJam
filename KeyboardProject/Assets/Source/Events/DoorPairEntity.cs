using System;
using UnityEngine;

namespace Assets.Source.Events
{
    public class DoorPairEntity : ActionEntity
    {
        [SerializeField] private DoorEntity m_Door1;
        [SerializeField] private DoorEntity m_Door2;


        // Start is called before the first frame update
        void Start()
        {
            m_Word = WordEventManager.Instance.GetRandomWord();

            m_Door1.SetWord(m_Word);
            m_Door2.SetWord(m_Word);

            m_Door1.Activate();
            m_Door2.DeActivate();
        }
        void Update()
        {

            if (!String.IsNullOrWhiteSpace(m_Word))
            {
                if (WordEventManager.Instance.HasInputedWord(m_Word))
                {
                    Toggle();
                    WordEventManager.Instance.BufferReset();
                }
            }

        }


        public override void Toggle()
        {
            // Pick New Random Word
            m_Word = WordEventManager.Instance.GetRandomWord();

            m_Door1.Toggle();
            m_Door2.Toggle();
            m_Door1.SetWord(m_Word);
            m_Door2.SetWord(m_Word);
        }

        public override void Activate()
        {
            m_Door1.Activate();
            m_Door2.DeActivate();
        }

        public override void DeActivate()
        {
            m_Door1.DeActivate();
            m_Door2.Activate();
        }
    }
}
