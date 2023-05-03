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
    
    [Header("UI")]
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

    [Header("References")] 
    public GameObject menuButton;
    public GameObject resumeButton;
    public GameObject resetButton;
    public GameObject restartButton;
    public GameObject startTutorialButton;
    public GameObject exitButton;
    
    [Header("Controllers")]
    public GameObject linePointer;

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
                    UpdateLine(menuButton, openMenuInfo);
                    break;
                case State.Resume:
                    buttonLeft.SetActive(false);
                    buttonRight.SetActive(true);
                    ActivateUI(resumeInfo);
                    UpdateLine(resumeButton, resumeInfo);

                    break;
                case State.ResetObjects:
                    buttonLeft.SetActive(true);
                    ActivateUI(resetInfo);
                    UpdateLine(resetButton, resetInfo);

                    break;
                case State.RestartGame:
                    ActivateUI(restartInfo);
                    UpdateLine(restartButton, restartInfo);

                    break;
                case State.StartTutorial:
                    ActivateUI(startTutorialInfo);
                    UpdateLine(startTutorialButton, startTutorialInfo);

                    break;
                case State.ExitGame:
                    ActivateUI(exitInfo);
                    UpdateLine(exitButton, exitInfo);

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
        
        linePointer.SetActive(false);

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

    private void UpdateLine(GameObject o1, GameObject o2)
    {
        var linePointerController = linePointer.GetComponent<LinePointerController>();
        
        linePointerController.button = o1;
        linePointerController.explanationText = o2;
    }
}
