using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    [Range(1, 50)]
    private float m_movementSpeed;

    private bool m_movementActionHeld = false;
    private Vector2 m_movementLastInputValue;

    // Constants
    private const float MOVEMENT_TO_FORCE_MULTIPLIER = 500f;

    [SerializeField]
    private bool m_ignoreSpawner;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        transform.SetParent(GameController.Instance.PlayerParent, false);
        if (!m_ignoreSpawner)
        {
            transform.position = GameController.Instance.GetPlayerSpawnPosition();
        }
    }

    void Update()
    {
        if (m_movementActionHeld && !(m_movementLastInputValue == new Vector2(0, 0)))
        {
            Vector2 movement = m_movementLastInputValue * m_movementSpeed * MOVEMENT_TO_FORCE_MULTIPLIER * Time.deltaTime;
            m_rigidbody.AddForce(movement);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //code for when action starts (key down)
            //Debug.Log("Move Input Started");
            m_movementActionHeld = true;
        }
        else if (context.performed)
        {
            //code for when action is executed
            //Debug.Log("Move Input Performed");
            m_movementLastInputValue = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            //code for when action is completed or stopped (key up)
            //Debug.Log("Move Input Cancelled");
            m_movementActionHeld = false;
        }
    }
}
