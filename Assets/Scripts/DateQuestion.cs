using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateQuestion : MonoBehaviour
{
    [SerializeField]
    private TextToSpeech m_textToSpeech;
    [SerializeField]
    private Text m_text;

    void Start()
    {
        GameController.Instance.SentenceManager.NewSentenceEvent += OnNewSentence;
        if (GameController.Instance.SentenceManager.CurrentSentence != null)
            OnNewSentence(GameController.Instance.SentenceManager.CurrentSentence.Question);
    }

    private void OnDestroy()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.SentenceManager.NewSentenceEvent -= OnNewSentence;
        }
    }

    // Update is called once per frame
    void OnNewSentence(string question)
    {
        m_text.text = question;
        m_textToSpeech.RunTTS(question);
    }
}
