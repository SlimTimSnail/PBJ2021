using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> m_spawnLocations;

    private int m_currentSpawnIndex;

    public Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = m_spawnLocations[m_currentSpawnIndex].transform.position;
        m_currentSpawnIndex = (m_currentSpawnIndex + 1) % m_spawnLocations.Count;

        return spawnPosition;
    }
}
