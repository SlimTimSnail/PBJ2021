using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

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
        string url = m_urlPrefix + UnityWebRequest.EscapeURL(textToSpeechWords);
        Debug.LogFormat("TTS with Url = {0}", url);
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogFormat("TTS error: {0}", www.error);
                m_audioSource.clip = null;
            }
            else
            {
                Debug.Log("TTS request successful");
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                if (clip != null)
                {
                    m_audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
                    m_audioSource.Play();
                }
                else
                {
                    Debug.LogError("TTS result null");
                }
            }

        }
    }
}
