using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public KeyCode keyToHold;
    public float holdTime = 3f;

    public GameObject menuUI;
    public GameObject menuVisuals;
    public SmoothLocomotion smoothLocomotion;
    public ObjectResseter objectResseter;

    private bool keyHeld = false;
    private bool isPause = false;
    private float heldTime = 0f;

    void FixedUpdate()
    {
        //if (Input.GetKey(keyToHold))
        if (InputBridge.Instance.StartButton && !isPause)
        {
            heldTime += Time.deltaTime;
            if (heldTime >= holdTime && !keyHeld)
            {
                keyHeld = true;
                Debug.Log("You held down the " + keyToHold.ToString() + " key for at least " + holdTime + " seconds!");

                if (!isPause)
                    Pause();
                else
                    Resume();
            }
        }
        else
        {
            keyHeld = false;
            heldTime = 0f;
        }
    }

    public void Resume()
    {
        isPause = false;
        
        menuUI.SetActive(false);
        menuVisuals.SetActive(false);
        smoothLocomotion.enabled = true;
    }

    public void Pause()
    {
        isPause = true;
        menuUI.SetActive(true);
        menuVisuals.SetActive(true);
        smoothLocomotion.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetObjects()
    {
        objectResseter.ResetObjects();
    }
}