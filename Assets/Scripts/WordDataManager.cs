using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordDataManager : MonoBehaviour
{
    [SerializeField]
    private List<Word> m_wordDataList;

    private Dictionary<WordObject.WordLength, List<Word>> m_wordLengthMap = new Dictionary<WordObject.WordLength, List<Word>>();

    private void Start()
    {
        for (int i = 0; i < m_wordDataList.Count; i++)
        {
            Word word = m_wordDataList[i];
            var length = word.Length;
            if (!m_wordLengthMap.ContainsKey(length))
            {
                m_wordLengthMap.Add(length, new List<Word>());
            }
            m_wordLengthMap[length].Add(word);
        }
    }

    public Word GetNextWordData(WordObject.WordLength length)
    {
        List<Word> wordList = m_wordLengthMap[length];
        if (wordList.Count < 1) throw new InvalidOperationException($"Have used all words for length: {length}");

        int randomIndex = UnityEngine.Random.Range(0, wordList.Count);
        Word chosenword = wordList[randomIndex];
        wordList.RemoveAt(randomIndex);

        return chosenword;
    }
}
