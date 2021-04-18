using UnityEngine;

[CreateAssetMenu]
public class Sentence : ScriptableObject
{
    public string Question => m_question;
    public Word[] ValidWords => m_validWords;
    public Word[] InvalidWords => m_invalidWords;

    [SerializeField]
    private string m_question;
    [SerializeField]
    private Word[] m_validWords;
    [SerializeField]
    private Word[] m_invalidWords;


    public bool IsValidWord(Word word)
    {
        foreach (Word w in m_validWords)
        {
            if (w == word) return true;
        }
        return false;
    }

}
