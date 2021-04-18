using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum SentenceState
{
    Incomplete,
    Complete,
}

public class SentenceManager : MonoBehaviour
{
    [SerializeField]
    private Sentence[] m_sentences;
    [SerializeField]
    private UnityEvent m_sentenceComplete;

    [SerializeField]
    public System.Action<string> NewSentenceEvent;
    [SerializeField]
    public System.Action<List<Word>, SentenceState> WordScoredEvent;
    [SerializeField]
    public System.Action<int> PointsEvent;


    private int m_currentSentence = -1;
    public Sentence CurrentSentence => m_currentSentence >= 0 ? m_sentences[m_currentSentence] : null;
    
    private SentenceState m_sentenceState;
    public SentenceState SentenceState => m_sentenceState;

    private WordPool m_subjectPool;
    private WordPool m_verbPool;
    private WordPool m_objectPool;
    private Word m_currentWord;

    private List<Word> m_formedSentence;
    private bool m_subjectSpoken;
    private bool m_verbSpoken;
    private bool m_objectSpoken;
    private bool m_correctOrder;
    
    private int m_nextWord = 0;
    private int m_points = 0;

    [SerializeField]
    private int m_losePointThreshold;

    public void OnEnable()
    {
        GameController.Instance.GameStateEnterEvent += OnGameStateEntered;
    }
    public void OnDisable()
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
                NewSentence();
                break;
            case GameState.Lose_Level:
                NewSentence();
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        NewSentence();
    }

    public void NewSentence()
    {
        ++m_currentSentence;

        if (m_currentSentence >= m_sentences.Length)
        {
            SceneManager.LoadScene("StartScene");
            return;
        }

        m_formedSentence = new List<Word>();

        m_subjectPool = new WordPool();
        m_verbPool = new WordPool();
        m_objectPool = new WordPool();

        foreach (Word w in CurrentSentence.ValidWords)
        {
            switch (w.Category)
            {
                case WordCategory.Subject:
                    m_subjectPool.AddWord(w);
                    break;
                case WordCategory.Verb:
                    m_verbPool.AddWord(w);
                    break;
                case WordCategory.Object:
                    m_objectPool.AddWord(w);
                    break;
            }
        }
        foreach (Word w in CurrentSentence.InvalidWords)
        {
            switch (w.Category)
            {
                case WordCategory.Subject:
                    m_subjectPool.AddWord(w);
                    break;
                case WordCategory.Verb:
                    m_verbPool.AddWord(w);
                    break;
                case WordCategory.Object:
                    m_objectPool.AddWord(w);
                    break;
            }
        }

        m_subjectPool.Shuffle();
        m_verbPool.Shuffle();
        m_objectPool.Shuffle();      

        m_subjectSpoken = false;
        m_verbSpoken = false;
        m_objectSpoken = false;
        m_correctOrder = true;
        m_sentenceState = SentenceState.Incomplete;

        m_nextWord = Random.Range(0, 3);
        GetNextWord();

        NewSentenceEvent?.Invoke(CurrentSentence.Question);
    }

    public Word GetNextWord()
    {
        Word ret = m_currentWord;
        m_currentWord = null;

        for (int i = 0; i < 3 && m_currentWord == null; ++i)
        {
            switch (m_nextWord)
            {
                case 0:
                    m_currentWord = m_subjectPool.NextWord();
                    break;
                case 1:
                    m_currentWord = m_verbPool.NextWord();
                    break;
                case 2:
                    m_currentWord = m_objectPool.NextWord();
                    break;
            }
            m_nextWord = (m_nextWord + 1) % 3;
        }

        return ret;
    }

    
    public void WordScored(Word word)
    {
        if (word == null || m_sentenceState == SentenceState.Complete) return;

        m_formedSentence.Add(word);
        bool isValid = CurrentSentence.IsValidWord(word);

        switch (word.Category)
        {
            case WordCategory.Subject:
                if (m_subjectSpoken)
                {
                    isValid = false;
                }
                m_subjectSpoken = true;
                break;
            case WordCategory.Verb:
                if (m_verbSpoken)
                {
                    isValid = false;
                }
                if (!m_subjectSpoken)
                {
                    m_correctOrder = false;
                }
                m_verbSpoken = true;
                break;
            case WordCategory.Object:
                if (m_objectSpoken)
                {
                    isValid = false;            
                }
                if (!m_subjectSpoken || !m_verbSpoken)
                {
                    m_correctOrder = false;
                }
                m_objectSpoken = true;
                break;
        }

        if (isValid)
            ++m_points;
        else
            --m_points;

        PointsEvent?.Invoke(m_points);

        if (m_subjectSpoken && m_verbSpoken && m_objectSpoken)
        {
            m_sentenceState = SentenceState.Complete;
            if (m_correctOrder)
            {
                ++m_points;
                PointsEvent?.Invoke(m_points);
            }
            m_sentenceComplete.Invoke();
        }

        WordScoredEvent?.Invoke(m_formedSentence, m_sentenceState);

        if (m_sentenceState == SentenceState.Complete)
        {
            if (m_points <= m_losePointThreshold)
            {
                GameController.Instance.CurrentGameState = GameState.Lose_Level;
            }
            else
            {
                GameController.Instance.CurrentGameState = GameState.Win_Level;
            }
        }
    }
}
