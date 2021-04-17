using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTimer : MonoBehaviour
{
    private IGetSpawnObject m_spawnWhichObject;

    [SerializeField]
    private float m_secondsInterval;

    private float m_nextTime;

    private float m_currentTimer;


    private void Awake()
    {
        m_nextTime = Time.time + m_secondsInterval;
        m_spawnWhichObject = GetComponent<IGetSpawnObject>();
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
        GameObject objectToSpawn = m_spawnWhichObject.GetObjectToSpawn();

        Instantiate(objectToSpawn);
    }
}
