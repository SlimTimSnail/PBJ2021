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

        [SerializeField]
        private WordCategory m_category;
        [SerializeField]
        private Word m_word;
        [SerializeField, Tooltip("Allow Replacement")]
        private bool m_allowReplacement;
    }

    public SentenceWord[] CorrectSentence => m_correctSentence;
    public Word[] MiscWords => m_miscWords;

    [SerializeField]
    private SentenceWord[] m_correctSentence;

    [SerializeField]
    private Word[] m_miscWords;
}
