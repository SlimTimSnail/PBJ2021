using UnityEngine;

[CreateAssetMenu]
public class Sentence : ScriptableObject
{
    [System.Serializable]
    public class SentenceWord
    {
        public WordCategory Category => m_category;
        public Word Word => m_word;
        public bool AllowReplacement => m_allowReplacement;

        [System.NonSerialized]
        public bool Spoken = false;

        [SerializeField]
        private WordCategory m_category;
        [SerializeField]
        private Word m_word;
        [SerializeField, Tooltip("Allow Replacement")]
        private bool m_allowReplacement;

    }

    public SentenceWord[] CorrectSentence => m_correctSentence;
    public Word[] MiscWords => m_miscWords;

    public bool IsComplete
    {
        get
        {
            foreach (SentenceWord w in m_correctSentence)
            {
                if (!w.Spoken) return false;
            }
            return true;
        }
    }

    [SerializeField]
    private SentenceWord[] m_correctSentence;

    [SerializeField]
    private Word[] m_miscWords;

    public void OnEnable()
    {
        ResetWords();
    }

    public void ResetWords()
    {
        foreach (SentenceWord w in m_correctSentence)
        {
            w.Spoken = false;
        }
    }

    public void SpeakWord(Word word)
    {
        foreach (SentenceWord w in m_correctSentence)
        {
            if (w.Word == word)
            {
                w.Spoken = true;
                return;
            }
        }
    }
}
