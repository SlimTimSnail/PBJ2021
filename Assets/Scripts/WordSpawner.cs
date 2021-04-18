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

    private SentenceManager m_manager;


    private void Start()
    {
        m_nextTime = Time.time + m_secondsInterval;
        m_manager = GameController.Instance.SentenceManager;
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
        if (m_manager.SentenceState == SentenceState.Complete) return;

        Word word = m_manager.GetNextWord();
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
