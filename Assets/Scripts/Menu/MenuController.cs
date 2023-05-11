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
    public TutorialSequenceController tutorialSequenceController;
    public BlurController blurController;
    private bool keyHeld = false;
    public bool isPause = false;
    private float heldTime = 0f;

    void FixedUpdate()
    {
        if (InputBridge.Instance.StartButton)
        {
            heldTime += Time.deltaTime;
            if (heldTime >= holdTime && !keyHeld)
            {
                keyHeld = true;

                isPause = !isPause;
                if (isPause)
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
        print("resume");
        isPause = false;
        
        menuUI.SetActive(false);
        menuVisuals.SetActive(false);
        smoothLocomotion.enabled = true;
        blurController.FadeOut();
    }

    public void Pause()
    {
        print("pause");

        isPause = true;
        menuUI.SetActive(true);
        menuVisuals.SetActive(true);
        smoothLocomotion.enabled = false;
        blurController.FadeIn();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetObjects()
    {
        objectResseter.ResetObjects();
        Resume();
    }

    public void StartTutorial()
    {
        Resume();
        tutorialSequenceController.StartTutorial();
    }

    public void Exit()
    {
        Application.Quit();
    }
}