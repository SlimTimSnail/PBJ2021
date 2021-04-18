using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordObject : MonoBehaviour
{
    [SerializeField]
    private WordLength m_length;

    public  Word Word => m_word;

    [SerializeField]
    private Text m_text;
    [SerializeField]
    private Graphic m_color;
    [SerializeField]
    private Color m_subject;
    [SerializeField]
    private Color m_object;
    [SerializeField]
    private Color m_verb;


    private Word m_word;

    private void Awake()
    {
        Setup(GameController.Instance.SentenceManager.GetNextWord(m_length));
    }


    public void Setup(Word word)
    {
        m_word = word;
        m_text.text = m_word.Text;
        if (m_color != null)
        {
            switch (word.Category)
            {
                case WordCategory.Subject:
                    m_color.color = m_subject;
                    break;
                case WordCategory.Object:
                    m_color.color = m_object;
                    break;
                case WordCategory.Verb:
                    m_color.color = m_verb;
                    break;
            }
        }
    }

#if UNITY_EDITOR
    private void Reset()
    {
        m_text = GetComponent<Text>();
    }
#endif
}
