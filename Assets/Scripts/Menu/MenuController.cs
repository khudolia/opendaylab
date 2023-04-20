using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public KeyCode keyToHold;
    public float holdTime = 3f;

    public GameObject menuUI;
    public SmoothLocomotion smoothLocomotion;

    private bool keyHeld = false;
    private float heldTime = 0f;

    void FixedUpdate()
    {
        //if (Input.GetKey(keyToHold))
        if (InputBridge.Instance.StartButton)
        {
            heldTime += Time.deltaTime;
            if (heldTime >= holdTime && !keyHeld)
            {
                keyHeld = true;
                Debug.Log("You held down the " + keyToHold.ToString() + " key for at least " + holdTime + " seconds!");
                menuUI.SetActive(!menuUI.activeSelf);
                smoothLocomotion.enabled = !menuUI.activeSelf;
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