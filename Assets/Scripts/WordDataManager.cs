using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordDataManager : MonoBehaviour
{
    [SerializeField]
    private List<Word> m_wordDataList;

    [SerializeField]
    private int m_currentWordIndex;

    public Word GetNextWordData()
    {
        Word word = m_wordDataList[m_currentWordIndex];
        m_currentWordIndex = (m_currentWordIndex + 1) % m_wordDataList.Count;

        return word;
    }
}
