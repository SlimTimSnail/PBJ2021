using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordObject : MonoBehaviour
{
    public  Word Word => m_word;

    [SerializeField]
    private Text m_text;

    private Word m_word;

    private void Awake()
    {
        Setup(GameController.Instance.GetNextWordData());
    }


    public void Setup(Word word)
    {
        m_word = word;
        m_text.text = m_word.Text;
    }

#if UNITY_EDITOR
    private void Reset()
    {
        m_text = GetComponent<Text>();
    }
#endif
}
