using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupPlayerVisuals : MonoBehaviour
{
    private static int m_playerNumber = 0;

    [SerializeField]
    private List<Image> m_playerVisualPrefabList;
    [SerializeField]
    private Image m_defaultPlayerVisualPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Image playerVisual = (m_playerVisualPrefabList.Count > m_playerNumber)
            ? m_playerVisualPrefabList[m_playerNumber] : m_defaultPlayerVisualPrefab;

        Instantiate(playerVisual, transform);

        m_playerNumber++;
    }
}
