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


    private Sentence CurrentSentence => m_sentences[m_currentSentence];
    private int m_currentSentence = -1;

    private List<Word> m_formedSentence;

    // Start is called before the first frame update
    void Start()
    {
        NewSentence();
    }

    public void NewSentence()
    {
        ++m_currentSentence;
        m_formedSentence = new List<Word>();
        CurrentSentence.ResetWords();
    }



    
    public void WordScored(Word word)
    {
        m_formedSentence.Add(word);
        CurrentSentence.SpeakWord(word);
        if (CurrentSentence.IsComplete)
        {
            m_sentenceComplete.Invoke();
        }
    }

}
