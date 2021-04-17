using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TextToSpeech : MonoBehaviour
{
    string m_urlPrefix = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl=en&q=";

    private AudioSource m_audioSource;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void RunTTS(string textToSpeechWords)
    {
        StartCoroutine(GetTTS(textToSpeechWords));
    }
    private IEnumerator GetTTS(string textToSpeechWords)
    {
        Debug.Log(textToSpeechWords);
        // Remove the "spaces" in excess
        Regex rgx = new Regex("\\s+");
        // Replace the "spaces" with "% 20" for the link Can be interpreted
        string result = rgx.Replace(textToSpeechWords, "%20");
        Debug.Log(result);
        string url = m_urlPrefix + result;
        WWW www = new WWW(url);
        yield return www;
        m_audioSource.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        m_audioSource.Play();
        Debug.Log("TextToSpeech");
    }
}
