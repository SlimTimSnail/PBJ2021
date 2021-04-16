using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    #region Object Parents
    [SerializeField]
    private Transform m_playerParent;
    public Transform PlayerParent => m_playerParent;

    [SerializeField]
    private Transform m_wordParent;
    public Transform WordParent => m_wordParent;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;    
    }


}
