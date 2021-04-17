using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ClampVelocity : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;

    [SerializeField]
    private float m_maxSpeed;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        m_rigidbody2D.velocity = Vector2.ClampMagnitude(m_rigidbody2D.velocity, m_maxSpeed);
    }
}
