using System;
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
    public Action<List<Word>> WordScoredEvent;


    public Sentence CurrentSentence => m_sentences[m_currentSentence];
    private int m_currentSentence = -1;

    private WordPool m_shortWordPool;
    private WordPool m_mediumWordPool;
    private WordPool m_longWordPool;

    private List<Word> m_formedSentence;
    private bool m_subjectSpoken;
    private bool m_verbSpoken;
    private bool m_objectSpoken;
    private bool m_correctOrder;
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

        m_shortWordPool = new WordPool();
        m_mediumWordPool = new WordPool();
        m_longWordPool = new WordPool();

        foreach (Word w in CurrentSentence.ValidWords)
        {
            switch (w.Length)
            {
                case WordLength.Short:
                    m_shortWordPool.AddWord(w);
                    break;
                case WordLength.Medium:
                    m_mediumWordPool.AddWord(w);
                    break;
                case WordLength.Long:
                    m_longWordPool.AddWord(w);
                    break;
            }
        }
        foreach (Word w in CurrentSentence.InvalidWords)
        {
            switch (w.Length)
            {
                case WordLength.Short:
                    m_shortWordPool.AddWord(w);
                    break;
                case WordLength.Medium:
                    m_mediumWordPool.AddWord(w);
                    break;
                case WordLength.Long:
                    m_longWordPool.AddWord(w);
                    break;
            }
        }

        m_shortWordPool.Shuffle();
        m_mediumWordPool.Shuffle();
        m_longWordPool.Shuffle();      

        m_subjectSpoken = false;
        m_verbSpoken = false;
        m_correctOrder = false;
        m_correctOrder = true;
    }

    public Word GetNextWord(WordLength length)
    {
        switch (length)
        {
            case WordLength.Short:
                return m_shortWordPool.NextWord();
            case WordLength.Long:
                return m_longWordPool.NextWord();
            default:
                return m_mediumWordPool.NextWord();          
        }
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
                break;
        }

        if (isValid)
            ++m_points;


        if (m_subjectSpoken && m_verbSpoken && m_objectSpoken)
        {
            if (m_correctOrder)
                ++m_points;
            m_sentenceComplete.Invoke();
        }

        WordScoredEvent?.Invoke(m_formedSentence);
    }

}
