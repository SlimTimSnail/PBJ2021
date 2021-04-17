using UnityEngine;

[CreateAssetMenu]
public class Word :ScriptableObject
{
    public string Text => m_text;
    public float Points => m_points;
    public float Weight => m_weight;
    public WordCategory Category => m_category;

    public int MatchTags(Word other)
    {
        int matches = 0;
        foreach (string t in m_tags)
        {
            foreach (string r in other.m_tags)
            {
                if (t == r) ++matches;
            }
        }
        return matches;
    }


    [SerializeField, ContextMenuItem("Set to Name", "SetTextToName")]
    private string m_text;
    [SerializeField]
    private WordCategory m_category;
    [SerializeField, WordTag]
    private string[] m_tags;
    [SerializeField]
    private float m_points;
    [SerializeField]
    private float m_weight;

    [ContextMenu("Rename")]
    private void Rename()
    {
        name = m_text;
    }
    [ContextMenu("Rename", true)]
    private bool CanRename()
    {
        return !string.IsNullOrWhiteSpace(m_text);
    }
    private void SetTextToName()
    {
        m_text = name;
    }

#if UNITY_EDITOR
    private void Reset()
    {
        m_text = name;
    }
    private void OnValidate()
    {
        if (m_text == null) m_text = name;
    }
#endif
}
