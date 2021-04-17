using UnityEngine;
using UnityLibrary;

public class TestSpeech : MonoBehaviour
{
    public string sayAtStart = "Welcome!";

    // Start is called before the first frame update
    void OnEnable()
    {
        Speak();

    }

    void Speak()
    {
        Speech.instance.Say(sayAtStart, TTSCallback);
    }

    void TTSCallback(string message, AudioClip audio) {
        AudioSource source = GetComponent<AudioSource>();
        if(source == null) {
            source = gameObject.AddComponent<AudioSource>();
        }

        source.clip = audio;
        source.Play();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            Speak();
        }
    }
#endif
}
