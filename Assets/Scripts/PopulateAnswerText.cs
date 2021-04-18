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

    private string m_cleanText = null;

    [SerializeField]
    private TextToSpeech m_textToSpeech;

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
                ResetAnswer();
                break;
            case GameState.Win_Level:
                StartCoroutine(PlayTTSOnDelayCoroutine(1f));
                break;
            case GameState.Lose_Level:
                StartCoroutine(PlayTTSOnDelayCoroutine(1f));
                break;
            default:
                break;
        }
    }

    private IEnumerator PlayTTSOnDelayCoroutine(float secondsDelay)
    {
        yield return new WaitForSeconds(secondsDelay);
        m_textToSpeech.RunTTS(m_cleanText);
    }

    private void ResetAnswer()
    {
        m_text.text = "";
        m_cleanText = "";
    }

    private void WordScored(List<Word> currentSentence, SentenceState sentenceState)
    {
        string sentenceToDisplay = BuildSentenceString(currentSentence, sentenceState, true);
        m_text.text = sentenceToDisplay;

        m_cleanText = BuildSentenceString(currentSentence, sentenceState, false);
    }

    private string BuildSentenceString(List<Word> currentSentence, SentenceState sentenceState, bool includeColor)
    {
        m_sentenceBuilder.Clear();
        for (int i = 0; i < currentSentence.Count; i++)
        {
            Word w = currentSentence[i];

            string wordString = w.Text;

            if (includeColor)
            {
                switch (w.Category)
                {
                    case WordCategory.Subject:
                        m_sentenceBuilder.Append("<color=#FFB2B2>");
                        break;
                    case WordCategory.Verb:
                        m_sentenceBuilder.Append("<color=#B2FFB9>");
                        break;
                    case WordCategory.Object:
                        m_sentenceBuilder.Append("<color=#B2CDFF>");
                        break;
                }
            }
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
                if (includeColor) m_sentenceBuilder.Append("</color>");
            }
            else
            {
                m_sentenceBuilder.Append(" ");
                m_sentenceBuilder.Append(m_textInfo.ToLower(wordString));
                if (includeColor) m_sentenceBuilder.Append("</color>");

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
