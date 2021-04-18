using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    #region Object Parents
    [SerializeField]
    private Transform m_playerParent;
    public Transform PlayerParent => m_playerParent;

    [SerializeField]
    private Transform m_wordParent;
    public Transform WordParent => m_wordParent;
    #endregion

    [SerializeField]
    private GoalForWords m_wordGoal;
    public GoalForWords WordGoal => m_wordGoal;

    #region Spawn Controllers
    [SerializeField]
    private SpawnController m_playerSpawnController;
    public Vector3 GetPlayerSpawnPosition() => m_playerSpawnController.GetSpawnPosition();

    [SerializeField]
    private SpawnController m_wordSpawnController;
    public Vector3 GetWordSpawnPosition() => m_wordSpawnController.GetSpawnPosition();

    [SerializeField]
    private SpawnController m_bombSpawnController;
    public Vector3 GetBombSpawnPosition() => m_bombSpawnController.GetSpawnPosition();

    [SerializeField]
    private SentenceManager m_sentenceManager;
    public SentenceManager SentenceManager => m_sentenceManager;

    [SerializeField]
    private WordDataManager m_wordDataManager;
    public Word GetNextWordData(WordLength length) => m_wordDataManager.GetNextWordData(length);
    #endregion

    public Action<GameState> GameStateExitEvent;
    public Action<GameState> GameStateEnterEvent;
    private GameState m_currentGameState;
    public GameState CurrentGameState { get { return m_currentGameState; } set { SetGameState(value); } }
    private void SetGameState(GameState value)
    {
        if (value == m_currentGameState) return;

        GameStateExitEvent?.Invoke(m_currentGameState);
        m_currentGameState = value;
        GameStateEnterEvent?.Invoke(value);
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        m_currentGameState = GameState.Start;
    }

    private void Start()
    {
        GameStateEnterEvent?.Invoke(m_currentGameState);
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Player Joined");
        CurrentGameState = GameState.Playing;
    }
    private void OnEnable()
    {
        GameController.Instance.GameStateEnterEvent += OnGameStateEntered;
    }
    private void OnDisable()
    {
        GameController.Instance.GameStateEnterEvent -= OnGameStateEntered;
    }

    public void OnGameStateEntered(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.Playing:
                break;
            case GameState.Win_Level:
                StartCoroutine(MoveToStateCoroutine(5f));
                break;
            case GameState.Lose_Level:
                StartCoroutine(MoveToStateCoroutine(5f));
                break;
            default:
                break;
        }
    }

    private IEnumerator MoveToStateCoroutine(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        CurrentGameState = GameState.Playing;
    }

    public void Win()
    {
        CurrentGameState = GameState.Win_Level;
    }
}

public enum GameState
{
    Start,
    Playing,
    Win_Level,
    Lose_Level,
}
