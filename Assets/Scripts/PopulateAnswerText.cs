using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PopulateAnswerText : MonoBehaviour
{
    [SerializeField]
    private Text m_text; 
    
    StringBuilder m_sentenceBuilder = new StringBuilder();

    private void OnEnable()
    {
        GameController.Instance.SentenceManager.WordScoredEvent += WordScored;
    }
    private void OnDisable()
    {
        GameController.Instance.SentenceManager.WordScoredEvent -= WordScored;
    }

    private void WordScored(List<Word> currentSentence)
    {
        m_sentenceBuilder.Clear();
        for (int i = 0; i < currentSentence.Count; i++)
        {
            if (i > 0)
            {
                m_sentenceBuilder.Append(" ");
                m_sentenceBuilder.Append(currentSentence[i].Text.ToLower());
            }
            else
            {
                m_sentenceBuilder.Append(currentSentence[i].Text);
            }
        }
        m_text.text = m_sentenceBuilder.ToString();
    }
}
