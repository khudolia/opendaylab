using Helpers;
using UnityEngine;

public enum TutorialState
{
    None,
    PressAllButtons,
    Walk,
    GrabObject,
    Menu,
}

public class TutorialSequenceController : MonoBehaviour
{
    public TutorialState state;

    [Header("States UI")]
    public GameObject useUI;
    public GameObject walkUI;
    public GameObject menuUI;
    public GameObject allButtonsUI;

    private TutorialState _previousState;
    private GameObject _previousActivatedUI;

    private PressAllButtonsStep _allButtonsStep;
    private MoveStep _moveStep;
    private GrabAndUseObjectStep _grabAndUseObjectStep;
    
    private void Start()
    {
        _allButtonsStep = GetComponent<PressAllButtonsStep>();
        _moveStep = GetComponent<MoveStep>();
        _grabAndUseObjectStep = GetComponent<GrabAndUseObjectStep>();
        
        useUI.SetActive(false);
        walkUI.SetActive(false);
        menuUI.SetActive(false);
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
                    StartAllButtonsTutorial();
                    break;
                case TutorialState.Walk:
                    StartWalkTutorial();
                    break;
                case TutorialState.GrabObject:
                    StartGrabTutorial();
                    break;
                case TutorialState.Menu:
                    StartMenuTutorial();
                    break;
            }

            
            _previousState = state;
        }
    }

    public void StartTutorial()
    {
        state = TutorialState.GrabObject;
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
        ActivateUI(menuUI);    
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
