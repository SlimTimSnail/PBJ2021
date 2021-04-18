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
        GameController.Instance.GameStateEnterEvent += OnGameStateEntered;
    }
    private void OnDisable()
    {
        GameController.Instance.SentenceManager.WordScoredEvent -= WordScored;
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
                ResetAnswer();
                break;
            case GameState.Lose_Level:
                ResetAnswer();
                break;
            default:
                break;
        }
    }

    private void ResetAnswer()
    {
        m_text.text = "";
    }

    private void WordScored(List<Word> currentSentence, SentenceState sentenceState)
    {
        string sentenceToDisplay = BuildSentenceString(currentSentence, sentenceState);
        m_text.text = sentenceToDisplay;
    }

    private string BuildSentenceString(List<Word> currentSentence, SentenceState sentenceState)
    {
        m_sentenceBuilder.Clear();
        for (int i = 0; i < currentSentence.Count; i++)
        {
            string wordString = currentSentence[i].Text;
            if (i == 0)
            {
                if (wordString.Length == 1)
                {
                    m_sentenceBuilder.Append(wordString);
                }
                else
                {
                    m_sentenceBuilder.Append(m_textInfo.ToTitleCase(m_textInfo.ToLower(wordString)));
                }
            }
            else
            {
                m_sentenceBuilder.Append(" ");
                m_sentenceBuilder.Append(m_textInfo.ToLower(wordString));

                if (i == currentSentence.Count - 1)
                {
                    if (sentenceState == SentenceState.Complete)
                    {
                        m_sentenceBuilder.Append(".");
                    }
                }
            }
        }

        return m_sentenceBuilder.ToString();
    }
}
