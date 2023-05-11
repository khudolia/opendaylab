using System.Collections;
using System.Collections.Generic;
using Helpers;
using Tutorial;
using UnityEngine;

public enum TutorialState
{
    None,
    PressAllButtons,
    Menu,
    Walk,
    GrabObject,
    Finish
}

public class TutorialSequenceController : MonoBehaviour
{
    public TutorialState state;

    [Header("States UI")] public GameObject useUI;
    public GameObject finishUI;
    public List<GameObject> particles;

    [Header("Controllers")] public HandModelSelector handModelSelector;
    private TutorialState _previousState;
    private GameObject _previousActivatedUI;

    [Header("Settings")] public bool autoStart = false;

    private PressAllButtonsStep _allButtonsStep;
    private MoveStep _moveStep;
    private GrabAndUseObjectStep _grabAndUseObjectStep;
    private MenuStep _menuStep;

    private bool _isCanceled = false;
    private void Start()
    {
        _allButtonsStep = GetComponent<PressAllButtonsStep>();
        _moveStep = GetComponent<MoveStep>();
        _grabAndUseObjectStep = GetComponent<GrabAndUseObjectStep>();
        _menuStep = GetComponent<MenuStep>();

        useUI.SetActive(false);

        handModelSelector.SwapToHands();

        if(autoStart)
            StartTutorial();
    }

    void Update()
    {
        if(_isCanceled) return;
        
        if (state != _previousState)
        {
            switch (state)
            {
                case TutorialState.PressAllButtons:
                    handModelSelector.SwapToControllers();
                    StartAllButtonsTutorial();
                    break;
                case TutorialState.Menu:
                    StartMenuTutorial();
                    break;
                case TutorialState.Walk:
                    StartWalkTutorial();
                    break;
                case TutorialState.GrabObject:
                    handModelSelector.SwapToHands();
                    StartGrabTutorial();
                    break;
                case TutorialState.Finish:
                    StartCoroutine(DisplayFinishMessage());
                    break;
            }


            _previousState = state;
        }
    }

    public void StartTutorial()
    {
        _isCanceled = false;
        state = TutorialState.Walk;
    }

    public void CancelTutorial()
    {
        _isCanceled = true;
        useUI.SetActive(false);

        _menuStep.FinishTutorial();
        _moveStep.FinishTutorial();
        _grabAndUseObjectStep.FinishTutorial();

        handModelSelector.SwapToHands();
        state = TutorialState.None;
    }

    private void StartAllButtonsTutorial()
    {
        _allButtonsStep.StartTutorial();
        ActivateUI(null);
    }

    public void FinishAllButtonsTutorial()
    {
        state = state.Next();
    }

    private void StartWalkTutorial()
    {
        _moveStep.StartTutorial();
        ActivateUI(null);
    }

    public void FinishWalkTutorial()
    {
        state = state.Next();
    }

    private void StartGrabTutorial()
    {
        ActivateUI(null);
        _grabAndUseObjectStep.StartTutorial();
    }

    public void FinishGrabTutorial()
    {
        state = state.Next();
    }

    private void StartMenuTutorial()
    {
        _menuStep.StartTutorial();
    }

    public void FinishMenuTutorial()
    {
        state = state.Next();
    }

    private IEnumerator DisplayFinishMessage()
    {
        foreach (var particle in particles)
            particle.SetActive(true);

        finishUI.SetActive(true);
        yield return new WaitForSeconds(5);

        foreach (var particle in particles)
            particle.SetActive(false);

        finishUI.SetActive(false);
    }

    private void ActivateUI(GameObject ui)
    {
        if (_previousActivatedUI != null)
            _previousActivatedUI.SetActive(false);

        if (ui != null)
            ui.SetActive(true);

        _previousActivatedUI = ui;
    }
}