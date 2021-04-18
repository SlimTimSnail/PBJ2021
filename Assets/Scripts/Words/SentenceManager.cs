using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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


    public Sentence CurrentSentence => m_sentences[m_currentSentence];
    private int m_currentSentence = -1;

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
    private int m_points;

    // Start is called before the first frame update
    void Start()
    {
        NewSentence();
    }

    public void NewSentence()
    {
        ++m_currentSentence;
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

        m_nextWord = Random.Range(0, 3);
        GetNextWord();

        NewSentenceEvent?.Invoke(CurrentSentence.Question);
    }

    public WordLength GetNextWordLength()
    {
        return m_currentWord.Length;
    }

    public Word GetNextWord()
    {
        Word ret = m_currentWord;

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

        return ret;
    }

    
    public void WordScored(Word word)
    {
        if (word == null) return;

        m_formedSentence.Add(word);
        bool isValid = CurrentSentence.IsValidWord(word);

        switch (word.Category)
        {
            case WordCategory.Subject:
                if (m_subjectSpoken)
                {
                    m_correctOrder = false;
                    isValid = false;
                }
                m_subjectSpoken = true;
                break;
            case WordCategory.Verb:
                if (m_verbSpoken || !m_subjectSpoken)
                {
                    m_correctOrder = false;
                    isValid = false;
                }
                m_verbSpoken = true;
                break;
            case WordCategory.Object:
                if (m_objectSpoken || !m_subjectSpoken || !m_verbSpoken)
                {
                    m_correctOrder = false;
                    isValid = false;
                }
                m_objectSpoken = true;
                break;
        }

        if (isValid)
            ++m_points;

        SentenceState sentenceState = SentenceState.Incomplete;
        if (m_subjectSpoken && m_verbSpoken && m_objectSpoken)
        {
            sentenceState = SentenceState.Complete;
            if (m_correctOrder)
                ++m_points;
            m_sentenceComplete.Invoke();
        }

        WordScoredEvent?.Invoke(m_formedSentence, sentenceState);
    }

}
public enum SentenceState
{
    Incomplete,
    Complete,
}
