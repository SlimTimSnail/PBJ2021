using UnityEngine;

[CreateAssetMenu]
public class Sentence : ScriptableObject
{
    public Word[] ValidWords => m_validWords;
    public Word[] InvalidWords => m_invalidWords;

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
