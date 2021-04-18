using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalForWords : MonoBehaviour
{
    [SerializeField]
    private TextToSpeech m_textToSpeech;
    [SerializeField]
    private GameObject m_closed;
    [SerializeField]
    private GameObject m_open;

    private void Start()
    {
        GameController.Instance.GameStateEnterEvent += OnGameStateChange;
    }

    void OnGameStateChange(GameState state)
    {
        bool open = state == GameState.Playing;
        m_open.SetActive(open);
        m_closed.SetActive(!open);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        WordObject word = collision.GetComponent<WordObject>();
        if (word != null)
        {
            GameController.Instance.SentenceManager.WordScored(word.Word);
            m_textToSpeech.RunTTS(word.Word.Text);
            Destroy(collision.gameObject);
        }


        WordMovement wordMovement = collision.gameObject.GetComponent<WordMovement>();
        if (wordMovement != null)
        {
            Debug.Log($"Word Entered Goal");
        }
        else
        {
            Debug.Log($"NonWord Entered Goal: {collision.gameObject.name}");
        }
    }
}
