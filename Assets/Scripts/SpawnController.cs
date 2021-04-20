using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> m_spawnLocations;

    private int m_currentSpawnIndex;

    [SerializeField]
    private bool m_selectRandomly;

    public Vector3 GetSpawnPosition()
    {
        if (m_selectRandomly)
        {
            return GetRandomSpawnPosition();
        }
        else
        {
            return GetSequentialSpawnPosition();
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int indexExcludingLast = UnityEngine.Random.Range(m_currentSpawnIndex + 1, m_spawnLocations.Count + m_currentSpawnIndex) % m_spawnLocations.Count;

        Vector3 spawnPosition = m_spawnLocations[indexExcludingLast].transform.position;
        m_currentSpawnIndex = indexExcludingLast;

        return spawnPosition;
    }

    private Vector3 GetSequentialSpawnPosition()
    {
        Vector3 spawnPosition = m_spawnLocations[m_currentSpawnIndex].transform.position;
        m_currentSpawnIndex = (m_currentSpawnIndex + 1) % m_spawnLocations.Count;

        return spawnPosition;
    }
}
