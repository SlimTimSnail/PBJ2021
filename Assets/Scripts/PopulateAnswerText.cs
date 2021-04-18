using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PopulateAnswerText : MonoBehaviour
{
    [SerializeField]
    private Text m_text; 
    
    private StringBuilder m_sentenceBuilder = new StringBuilder();

    private TextInfo m_textInfo = CultureInfo.CurrentCulture.TextInfo;

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
            if (i == 0)
            {
                m_sentenceBuilder.Append(m_textInfo.ToTitleCase(m_textInfo.ToLower(currentSentence[i].Text)));
            }
            else
            {
                m_sentenceBuilder.Append(" ");
                m_sentenceBuilder.Append(m_textInfo.ToLower(currentSentence[i].Text));
            }
        }
        m_text.text = m_sentenceBuilder.ToString();
    }
}
