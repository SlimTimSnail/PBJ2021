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
    private SentenceManager m_sentenceManager;
    public SentenceManager SentenceManager => m_sentenceManager;

    [SerializeField]
    private WordDataManager m_wordDataManager;
    public Word GetNextWordData(WordLength length) => m_wordDataManager.GetNextWordData(length);
    #endregion

    private GameState m_currentGameState;
    public GameState CurrentGameState => m_currentGameState;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        m_currentGameState = GameState.Start;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Player Joined");
        m_currentGameState = GameState.Playing;
    }
}

public enum GameState
{
    Start,
    Playing,
    Win_Level,
    Lose_Level,
}
