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
    public GameObject walkUI;
    public GameObject allButtonsUI;
    public GameObject finishUI;
    public List<GameObject> particles;

    [Header("Controllers")] public HandModelSelector handModelSelector;
    private TutorialState _previousState;
    private GameObject _previousActivatedUI;

    private PressAllButtonsStep _allButtonsStep;
    private MoveStep _moveStep;
    private GrabAndUseObjectStep _grabAndUseObjectStep;
    private MenuStep _menuStep;


    private void Start()
    {
        _allButtonsStep = GetComponent<PressAllButtonsStep>();
        _moveStep = GetComponent<MoveStep>();
        _grabAndUseObjectStep = GetComponent<GrabAndUseObjectStep>();
        _menuStep = GetComponent<MenuStep>();

        useUI.SetActive(false);
        walkUI.SetActive(false);
        allButtonsUI.SetActive(false);

        StartTutorial();
    }

    void Update()
    {
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
        state = TutorialState.PressAllButtons;
    }

    private void StartAllButtonsTutorial()
    {
        _allButtonsStep.StartTutorial();
        ActivateUI(allButtonsUI);
    }

    public void FinishAllButtonsTutorial()
    {
        state = state.Next();
        allButtonsUI.SetActive(false);
    }

    private void StartWalkTutorial()
    {
        _moveStep.StartTutorial();
        ActivateUI(walkUI);
    }

    public void FinishWalkTutorial()
    {
        state = state.Next();
        walkUI.SetActive(false);
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