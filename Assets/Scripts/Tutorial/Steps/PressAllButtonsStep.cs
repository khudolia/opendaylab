using BNG;
using Helpers;
using UnityEngine;

public enum Button
{
    None,
    Trigger,
    Side,
    Touchpad,
    Menu,
    Finished
}

public class PressAllButtonsStep : MonoBehaviour
{
    public GameObject controllerTutorialLeft;
    public GameObject controllerTutorialRight;

    public SmoothLocomotion SmoothLocomotion;

    public Button currentState;
    private Button _previousState = Button.None;

    void Start()
    {
        controllerTutorialLeft.SetActive(false);
        controllerTutorialRight.SetActive(false);
        enabled = false;

    }

    void Update()
    {
        switch (currentState)
        {
            case Button.Trigger:
                TriggerButton();
                break;
            case Button.Side:
                SideButton();
                break;
            case Button.Touchpad:
                TouchpadButton();
                break;
            case Button.Menu:
                MenuButton();
                break;
            case Button.Finished:
                Finish();
                break;
        }
    }
    
    
    public void StartTutorial()
    {
        enabled = true;
        SmoothLocomotion.enabled = false;
        currentState = Button.Trigger;
    }

    private void TriggerButton()
    {
        EnableHelper(CurrentPart.Trigger);

        if (InputBridge.Instance.RightTriggerDown || InputBridge.Instance.LeftTriggerDown)
        {
            currentState = currentState.Next();
        }
    }

    private void SideButton()
    {
        EnableHelper(CurrentPart.Side);

        if (InputBridge.Instance.RightGripDown || InputBridge.Instance.LeftGripDown)
        {
            currentState = currentState.Next();
        }
    }

    private void TouchpadButton()
    {
        EnableHelper(CurrentPart.Touchpad);

        if (InputBridge.Instance.RightThumbstickAxis != Vector2.zero ||
            InputBridge.Instance.LeftThumbstickAxis != Vector2.zero)
        {
            currentState = currentState.Next();
        }
    }

    private void MenuButton()
    {
        EnableHelper(CurrentPart.Menu);

        if (InputBridge.Instance.StartButtonDown || InputBridge.Instance.BackButtonDown)
        {
            currentState = currentState.Next();
        }
    }

    private void Finish()
    {
        controllerTutorialLeft.SetActive(false);
        controllerTutorialRight.SetActive(false);

        SmoothLocomotion.enabled = true;
        GetComponent<TutorialSequenceController>().FinishAllButtonsTutorial();
        enabled = false;
    }

    private void EnableHelper(CurrentPart part)
    {
        if (!controllerTutorialLeft.activeSelf)
        {
            controllerTutorialLeft.SetActive(true);
            controllerTutorialRight.SetActive(true);
        }

        controllerTutorialLeft.GetComponent<ViveTutorialController>().part = part;
        controllerTutorialRight.GetComponent<ViveTutorialController>().part = part;
    }
}