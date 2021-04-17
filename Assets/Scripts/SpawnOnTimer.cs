using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnWhichObject))]
public class SpawnOnTimer : MonoBehaviour
{
    private SpawnWhichObject m_spawnWhichObject;

    [SerializeField]
    private float m_secondsInterval;

    private float m_nextTime;

    private float m_currentTimer;


    private void Awake()
    {
        m_spawnWhichObject = GetComponent<SpawnWhichObject>();
    }

    // Update is called once per frame
    void Update()
    {
        while (Time.realtimeSinceStartup >= m_nextTime)
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
