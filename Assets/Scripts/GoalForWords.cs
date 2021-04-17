using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalForWords : MonoBehaviour
{


    public void OnTriggerEnter2D(Collider2D collision)
    {
        WordMovement wordMovement = collision.gameObject.GetComponent<WordMovement>();
        if (wordMovement != null)
        {
            Debug.Log($"Word Entered Goal");
        }
        else
        {
            Debug.Log($"NonWord Entered Goal: {collision.gameObject.name}");
        }
    }
}
