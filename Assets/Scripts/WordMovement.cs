using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private bool m_ignoreSpawner;

    [SerializeField]
    private Vector2 m_initialForce;

    [SerializeField]
    private Vector2 m_constantForce;

    [SerializeField]
    private bool m_randomStartingRotation;

    [SerializeField]
    private ForcePatternData m_movementPatternData;

    private float m_forcePatternTime;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_forcePatternTime = UnityEngine.Random.Range(0f, 1f);

        transform.SetParent(GameController.Instance.WordParent, false);
        if (!m_ignoreSpawner)
        {
            transform.position = GameController.Instance.GetWordSpawnPosition();
            m_rigidbody.AddForce(m_initialForce * m_rigidbody.mass);
        }

        if (m_randomStartingRotation)
        {
            float randomRotation = UnityEngine.Random.Range(0f, 360f);
            transform.eulerAngles = new Vector3(0f, 0f, randomRotation);
        }
    }

    private void FixedUpdate()
    {
        m_rigidbody.AddForce(m_constantForce * m_rigidbody.mass);

        if (m_movementPatternData != null && !m_ignoreSpawner)
        {
            Vector2 forcePatternNow = m_movementPatternData.GetForceAtTime(m_forcePatternTime);
            m_rigidbody.AddForce(forcePatternNow);
        }
    }

    private void Update()
    {
        m_forcePatternTime += Time.deltaTime;
    }
}
