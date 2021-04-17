using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Word GetNextWordData() => m_wordDataManager.GetNextWordData();
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;    
    }


}
