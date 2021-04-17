using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WordTags : ScriptableObject, IEnumerable<string>
{
    [SerializeField]
    private string[] m_tags;

    public string this[int index]
    {
        get
        {
            return m_tags[index];
        }
    }

    public string[] AllTags => m_tags;

    public IEnumerator<string> GetEnumerator()
    {
        return ((IEnumerable<string>)m_tags).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return m_tags.GetEnumerator();
    }
}
