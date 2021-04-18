using System;
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

    private void OnEnable()
    {
        GameController.Instance.GameStateEnterEvent += GameStateEntered;
    }

    private void OnDisable()
    {
        GameController.Instance.GameStateEnterEvent -= GameStateEntered;
    }

    private void GameStateEntered(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                enabled = false;
                break;
            case GameState.Playing:
                enabled = true;
                break;
            case GameState.Win_Level:
                StopMovement();
                break;
            case GameState.Lose_Level:
                StopMovement();
                break;
            default:
                break;
        }
    }

    private void StopMovement()
    {
        m_rigidbody.velocity = new Vector2();
        m_rigidbody.angularVelocity = 0f;
    }

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
        if (GameController.Instance.CurrentGameState == GameState.Playing)
        {
            m_rigidbody.AddForce(m_constantForce * m_rigidbody.mass);

            if (m_movementPatternData != null && !m_ignoreSpawner)
            {
                Vector2 forcePatternNow = m_movementPatternData.GetForceAtTime(m_forcePatternTime);
                m_rigidbody.AddForce(forcePatternNow * m_rigidbody.mass);
            }
        }
    }

    private void Update()
    {
        if (GameController.Instance.CurrentGameState == GameState.Playing)
        {
            m_forcePatternTime += Time.deltaTime;
        }
    }
}
