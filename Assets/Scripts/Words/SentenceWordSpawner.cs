using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentenceWordSpawner : MonoBehaviour, IGetSpawnObject
{
    [SerializeField]
    private GameObject m_smallWord;
    [SerializeField]
    private GameObject m_medWord;
    [SerializeField]
    private GameObject m_largeWord;


    public GameObject GetObjectToSpawn()
    {

       // GameObject spawnObject = base.GetObjectToSpawn();
       // spawnObject.G

        return null;
    }
}
