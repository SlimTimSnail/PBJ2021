using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SetupPlayerVisuals : MonoBehaviour
{
    [SerializeField]
    private List<Image> m_playerVisualPrefabList;
    [SerializeField]
    private Image m_defaultPlayerVisualPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int playerCount = PlayerInputManager.instance.playerCount;
        Image playerVisual = (m_playerVisualPrefabList.Count >= playerCount)
            ? m_playerVisualPrefabList[playerCount] : m_defaultPlayerVisualPrefab;

        Instantiate(playerVisual, transform);
    }
}
