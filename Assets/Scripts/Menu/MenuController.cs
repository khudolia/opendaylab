using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public KeyCode keyToHold;
    public float holdTime = 3f;
    private bool keyHeld = false;
    private float heldTime = 0f;

    void FixedUpdate()
    {
        if (Input.GetKey(keyToHold))
        {
            heldTime += Time.deltaTime;
            if (heldTime >= holdTime && !keyHeld)
            {
                keyHeld = true;
                Debug.Log("You held down the " + keyToHold.ToString() + " key for at least " + holdTime + " seconds!");
                // insert your code to run here
            }
        }
        else
        {
            keyHeld = false;
            heldTime = 0f;
        }
    }
}