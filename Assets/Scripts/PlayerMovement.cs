using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    private float m_movementSpeed;

    private bool m_movementActionHeld = false;
    private Vector2 m_movementLastInputValue;

    // Start is called before the first frame update

    private void OnEnable()
    {
        /*
        m_movementAction = PlayerInputManager.instance.ac

        m_movementAction.started += x => m_movementActionHeld = true;
        m_movementAction.canceled += x => m_movementActionHeld = false;
        */
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_movementActionHeld && !(m_movementLastInputValue == new Vector2(0, 0)))
        {
            Vector2 movement = m_movementLastInputValue * m_movementSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //code for when action starts (key down)
            Debug.Log("Move Input Started");
            m_movementActionHeld = true;
        }
        else if (context.performed)
        {
            //code for when action is executed
            Debug.Log("Move Input Performed");
            m_movementLastInputValue = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            //code for when action is completed or stopped (key up)
            Debug.Log("Move Input Cancelled");
            m_movementActionHeld = false;
        }
    }
}
