using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnController))]
public class SpawnWhichObject : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_objectsToChooseFrom;

    private int m_currentObjectIndex;

    internal GameObject GetObjectToSpawn()
    {
        GameObject spawnObject = m_objectsToChooseFrom[m_currentObjectIndex];
        m_currentObjectIndex = (m_currentObjectIndex + 1) % m_objectsToChooseFrom.Count;

        return spawnObject;
    }
}
