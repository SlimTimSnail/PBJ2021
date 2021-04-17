using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnController))]
public class SpawnWhichObject : MonoBehaviour, IGetSpawnObject
{
    [SerializeField]
    protected List<GameObject> m_objectsToChooseFrom;

    private int m_currentObjectIndex;

    public GameObject GetObjectToSpawn()
    {
        GameObject spawnObject = m_objectsToChooseFrom[m_currentObjectIndex];
        m_currentObjectIndex = (m_currentObjectIndex + 1) % m_objectsToChooseFrom.Count;

        return spawnObject;
    }
}
