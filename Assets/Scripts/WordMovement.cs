using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private bool m_ignoreSpawner;

    [SerializeField]
    private Vector2 m_constantForce;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.SetParent(GameController.Instance.WordParent, false);
        if (!m_ignoreSpawner)
        {
            transform.position = GameController.Instance.GetWordSpawnPosition();
        }
    }

    private void FixedUpdate()
    {
        m_rigidbody.AddForce(m_constantForce);
    }
}