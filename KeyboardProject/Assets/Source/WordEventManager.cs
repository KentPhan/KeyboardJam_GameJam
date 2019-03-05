using System;
using System.Collections.Generic;
using Assets.Source.Events;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Assets.Source
{

    public enum GameState
    {
        START,
        PLAY,
        GAMEOVER
    }


    public class WordEventManager : MonoBehaviour
    {
        //[SerializeField] private ActionEntity[] m_Actions;
        private string m_CurrentInput;
        private bool m_BufferReset = false;



        [SerializeField] private RectTransform m_StartScreen;
        [SerializeField] private RectTransform m_GameOverScreen;


        private GameState m_CurrentState;
        private List<MonsterComponent> m_Monsters;
        private PlayerComponent m_Player;

        private string[] m_RandomWords = new string[]
        {
            "POOP",
            "FOOD",
            "QUIZ",
            "WIZ",
            "ELEPHANT",
            "RATS",
            "TALL",
        };


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

            m_CurrentState = GameState.START;
        }

        public void Start()
        {
            m_CurrentInput = string.Empty;
            m_BufferReset = false;

            m_StartScreen.gameObject.SetActive(true);
            m_GameOverScreen.gameObject.SetActive(false);
        }


        public void Update()
        {
            switch (m_CurrentState)
            {
                case GameState.START:
                    if (Input.GetButtonDown("Submit"))
                        StartGame();
                    break;
                case GameState.PLAY:
                    m_CurrentInput += Input.inputString;
                    break;
                case GameState.GAMEOVER:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            if (Input.GetKeyDown(KeyCode.Backspace))
                RestartGame();

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

        public void StartGame()
        {
            AIPathFindingManager.Instance.RescanGraph();


            // Get Player
            m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponent>();


            m_Monsters = new List<MonsterComponent>();
            GameObject[] m_MonsterGameObjects = GameObject.FindGameObjectsWithTag("Monster");

            foreach (var l_Monster in m_MonsterGameObjects)
            {
                MonsterComponent l_component = l_Monster.GetComponent<MonsterComponent>();
                l_component.SetTarget(m_Player);
                m_Monsters.Add(l_component);
            }

            m_StartScreen.gameObject.SetActive(false);
            m_CurrentState = GameState.PLAY;
        }


        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }

        public void TriggerGameOver()
        {
            m_GameOverScreen.gameObject.SetActive(true);
            m_CurrentState = GameState.GAMEOVER;
        }

        public bool HasInputedWord(string m_Word)
        {
            return m_CurrentInput.ToUpper().Contains(m_Word.ToUpper());
        }

        public string GetRandomWord()
        {
            return m_RandomWords[Random.Range(0, m_RandomWords.Length - 1)];
        }

        public void BufferReset()
        {
            m_BufferReset = true;
        }

    }
}
