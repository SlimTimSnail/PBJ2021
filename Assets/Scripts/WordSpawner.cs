using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_shortWord;
    [SerializeField]
    private GameObject m_medWord;
    [SerializeField]
    private GameObject m_longWord;

    [SerializeField]
    private float m_secondsInterval;


    private float m_nextTime;

    private float m_currentTimer;


    private void Awake()
    {
        m_nextTime = Time.time + m_secondsInterval;
    }

    // Update is called once per frame
    void Update()
    {
        while (Time.time >= m_nextTime)
        {
            Spawn();
            m_nextTime += m_secondsInterval;
        }
    }

    private void Spawn()
    {
        Word word = GameController.Instance.SentenceManager.GetNextWord();
        GameObject prefab = null;
        switch (word.Length)
        {
            case WordLength.Short:
                prefab = m_shortWord;
                break;
            case WordLength.Long:
                prefab = m_longWord;
                break;
            default:
                prefab = m_medWord;
                break;
        }

        GameObject wordObject = Instantiate(prefab);
        wordObject.GetComponent<WordObject>().Setup(word);
    }
}
