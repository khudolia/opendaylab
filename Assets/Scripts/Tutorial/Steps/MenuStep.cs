using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class MenuStep : MonoBehaviour
{
    enum State
    {
        None,
        OpenMenu,
        Resume,
        ResetObjects,
        RestartGame,
        StartTutorial,
        ExitGame,
        Finish
    }
    public MenuController menuController;
    public GameObject menuUI;
    public GameObject openMenuInfo;
    public GameObject resetInfo;
    public GameObject resumeInfo;
    public GameObject restartInfo;
    public GameObject startTutorialInfo;
    public GameObject exitInfo;

    public GameObject buttonLeft;
    public GameObject buttonRight;

    private State _state;
    private State _previousState;
    private GameObject _previousActivatedUI;

    private bool enteredPauseMode = false;
    private bool _previousEnteredPauseMode = false;
    // Start is called before the first frame update
    void Start()
    {
        menuUI.SetActive(false);
        openMenuInfo.SetActive(false);
        resumeInfo.SetActive(false);
        resetInfo.SetActive(false);
        restartInfo.SetActive(false);
        startTutorialInfo.SetActive(false);
        exitInfo.SetActive(false);
        
        buttonLeft.SetActive(false);
        buttonRight.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        enteredPauseMode = menuController.isPause;

        if (_previousEnteredPauseMode != enteredPauseMode && enteredPauseMode)
        {
            _state = State.Resume;

            _previousEnteredPauseMode = enteredPauseMode;
        }
        
        if (_state != _previousState)
        {
            switch (_state)
            {
                case State.OpenMenu:
                    ActivateUI(openMenuInfo);
                    break;
                case State.Resume:
                    buttonLeft.SetActive(false);
                    buttonRight.SetActive(true);
                    ActivateUI(resumeInfo);
                    break;
                case State.ResetObjects:
                    buttonLeft.SetActive(true);
                    ActivateUI(resetInfo);
                    break;
                case State.RestartGame:
                    ActivateUI(restartInfo);
                    break;
                case State.StartTutorial:
                    ActivateUI(startTutorialInfo);
                    break;
                case State.ExitGame:
                    ActivateUI(exitInfo);
                    break;
                case State.Finish:
                    FinishTutorial();
                    break;
            }

            
            _previousState = _state;
        }
    }
    
    public void StartTutorial()
    {
        menuUI.SetActive(true);
        _state = State.OpenMenu;
    }

    public void FinishTutorial()
    {
        menuController.Resume();
        
        menuUI.SetActive(false);
        openMenuInfo.SetActive(false);
        resumeInfo.SetActive(false);
        resetInfo.SetActive(false);
        restartInfo.SetActive(false);
        startTutorialInfo.SetActive(false);
        exitInfo.SetActive(false);
        
        buttonLeft.SetActive(false);
        buttonRight.SetActive(false);
        
        GetComponent<TutorialSequenceController>().FinishMenuTutorial();
        enabled = false;
    }

    public void NextStep()
    {
        _state = _state.Next();
    }

    public void PreviousStep()
    {
        _state = _state.Previous();
    }
    
    private void ActivateUI(GameObject ui)
    {
        if(_previousActivatedUI != null)
            _previousActivatedUI.SetActive(false);
        
        if(ui != null)
            ui.SetActive(true);
        
        _previousActivatedUI = ui;
    }
}
