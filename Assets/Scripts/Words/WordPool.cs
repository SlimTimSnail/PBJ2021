using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPool : IEnumerable<Word>
{
    private List<Word> m_words;
    private int m_currentWord;


    public WordPool()
    {
        m_words = new List<Word>();
        m_currentWord = -1;
    }
    public void Clear()
    {
        m_words.Clear();
    }
    public void AddWord(Word w)
    {
        m_words.Add(w);
    }
    public void Shuffle()
    {
        List<Word> newPool = new List<Word>();
        while (m_words.Count > 0)
        {
            int index = Random.Range(0, m_words.Count);
            newPool.Add(m_words[index]);
            m_words.RemoveAt(index);
        }
        m_words = newPool;
    }

    public Word NextWord()
    {
        if (Count == 0) return null;

        m_currentWord = (m_currentWord + 1) % m_words.Count;
        return m_words[m_currentWord];
    }

    public Word this[int index] => m_words[index];

    public IEnumerator<Word> GetEnumerator()
    {
        return ((IEnumerable<Word>)m_words).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)m_words).GetEnumerator();
    }

    public int Count => m_words.Count;
}
