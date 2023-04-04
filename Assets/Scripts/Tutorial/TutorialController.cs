using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialState
{
    Walk,
    GrabObject,
    UseObject,
    Menu,
}

public class TutorialController : MonoBehaviour
{
    public TutorialState state;

    [Header("States UI")]
    public GameObject grabUI;
    public GameObject useUI;
    public GameObject walkUI;
    public GameObject menuUI;

    [Header("Grab Object part")] public GameObject objectToBeHeld;
    
    private TutorialState _previousState;
    private GameObject _previousActivatedUI;

    private void Start()
    {
        grabUI.SetActive(false);
        useUI.SetActive(false);
        walkUI.SetActive(false);
        menuUI.SetActive(false);
    }

    void Update()
    {
        if (state != _previousState)
        {
            switch (state)
            {
                case TutorialState.Menu:
                    StartMenuTutorial();
                    break;
                case TutorialState.Walk:
                    StartWalkTutorial();
                    break;
                case TutorialState.GrabObject:
                    StartGrabTutorial();
                    break;
                case TutorialState.UseObject:
                    StartUseTutorial();
                    break;

            }

            
            _previousState = state;
        }
    }

    private void StartMenuTutorial()
    {
        ActivateUI(menuUI);    
    }

    private void StartGrabTutorial()
    {
        ActivateUI(grabUI);    
    }

    private void StartWalkTutorial()
    {
        ActivateUI(walkUI);    
    }

    private void StartUseTutorial()
    {
        ActivateUI(useUI);    
    }

    private void ActivateUI(GameObject ui)
    {
        if(_previousActivatedUI != null)
            _previousActivatedUI.SetActive(false);
        
        ui.SetActive(true);
        _previousActivatedUI = ui;
    }
}
