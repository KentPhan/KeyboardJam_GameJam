using System;
using System.Collections.Generic;
using Assets.Source.Events;
using TMPro;
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

        // Screens
        [SerializeField] private RectTransform m_StartScreen;
        [SerializeField] private RectTransform m_PlayScreen;
        [SerializeField] private TextMeshProUGUI m_SlimeCount;
        [SerializeField] private RectTransform m_GameOverScreen;


        // Input
        private string m_CurrentInput;
        private bool m_BufferReset = false;


        // Monster Stuff
        [SerializeField] private GameObject[] m_MonsterSpawnLocations;
        [SerializeField] private MonsterComponent m_MonsterPrefab;
        [SerializeField] private float m_SpawnTimer = 10.0f;
        [SerializeField] private float m_StartTimer = 8.0f;
        private float m_CurrentTimer = 10.0f;




        // Cache
        private GameState m_CurrentState;
        private List<MonsterComponent> m_Monsters;
        private PlayerComponent m_Player;
        private List<DoorPairEntity> m_DoorPairs;

        private string[] m_RandomWords = new string[]
        {
            "POOP",
            "FOOD",
            "QUIZ",
            "WIZ",
            "ELEPHANT",
            "RATS",
            "TALL",
            "YOLANDA",
            "78",
            "PANTS",
            "DICKS",
            "CHICKS",
            "Z41",
            "12345",
            "98765",
            "=3",
            "KENT",
            "+7",
            "FIVEZER00000",
            "APPLE",
            "JACK",
            "ESCALATE",
            "NOOOOOOOOOOOO"
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
            m_PlayScreen.gameObject.SetActive(false);
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
                    foreach (var l_Pair in m_DoorPairs)
                    {
                        if (HasInputedWord(l_Pair.GetWord()))
                        {
                            l_Pair.Toggle();
                            BufferReset();
                        }
                    }


                    m_CurrentTimer -= Time.deltaTime;

                    if (m_CurrentTimer <= 0.0f)
                    {
                        MonsterComponent l_NewMonster = Instantiate(m_MonsterPrefab, m_MonsterSpawnLocations[Random.Range(0, m_MonsterSpawnLocations.Length - 1)].transform.position,
                            Quaternion.identity, this.transform);
                        l_NewMonster.SetTarget(m_Player);
                        m_Monsters.Add(l_NewMonster);
                        m_CurrentTimer = m_SpawnTimer;


                        m_SlimeCount.text = m_Monsters.Count.ToString();
                    }


                    break;
                case GameState.GAMEOVER:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            if (Input.GetKeyDown(KeyCode.F1))
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


            // Get Door Pairs
            m_DoorPairs = new List<DoorPairEntity>();
            GameObject[] l_Doors = GameObject.FindGameObjectsWithTag("DoorPair");
            foreach (var l_Door in l_Doors)
            {
                DoorPairEntity l_component = l_Door.GetComponent<DoorPairEntity>();
                m_DoorPairs.Add(l_component);
            }


            // Get Monsters
            m_Monsters = new List<MonsterComponent>();
            GameObject[] m_MonsterGameObjects = GameObject.FindGameObjectsWithTag("Monster");

            foreach (var l_Monster in m_MonsterGameObjects)
            {
                MonsterComponent l_component = l_Monster.GetComponent<MonsterComponent>();
                l_component.gameObject.transform.SetParent(this.transform);
                l_component.SetTarget(m_Player);
                m_Monsters.Add(l_component);
            }

            // UI
            m_StartScreen.gameObject.SetActive(false);
            m_PlayScreen.gameObject.SetActive(true);
            m_CurrentState = GameState.PLAY;
            m_SlimeCount.text = "0";

            // Timer
            m_CurrentTimer = m_StartTimer;
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
