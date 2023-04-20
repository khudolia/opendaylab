using Helpers;
using UnityEngine;

public enum TutorialState
{
    None,
    PressAllButtons,
    Walk,
    GrabObject,
    UseObject,
    Menu,
}

public class TutorialSequenceController : MonoBehaviour
{
    public TutorialState state;

    [Header("States UI")]
    public GameObject grabUI;
    public GameObject useUI;
    public GameObject walkUI;
    public GameObject menuUI;
    public GameObject allButtonsUI;

    private TutorialState _previousState;
    private GameObject _previousActivatedUI;

    private PressAllButtonsStep _allButtonsStep;
    private void Start()
    {
        _allButtonsStep = GetComponent<PressAllButtonsStep>();
        
        grabUI.SetActive(false);
        useUI.SetActive(false);
        walkUI.SetActive(false);
        menuUI.SetActive(false);
        allButtonsUI.SetActive(false);
    }

    void Update()
    {
        if (state != _previousState)
        {
            switch (state)
            {
                case TutorialState.PressAllButtons:
                    StartAllButtonsTutorial();
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
                case TutorialState.Menu:
                    StartMenuTutorial();
                    break;
            }

            
            _previousState = state;
        }
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
