using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class GrabAndUseObjectStep : MonoBehaviour
{
    public enum State
    {
        None,
        Grab,
        Use
    }

    public HandController leftHand;
    public HandController rightHand;

    public GameObject grabUI;
    public GameObject useUI;

    public State state = State.None;

    private void Start()
    {
        grabUI.SetActive(false);
        useUI.SetActive(false);
        enabled = false;

    }

    void Update()
    {
        if (state == State.Grab)
        {
            if (leftHand.PreviousHeldObject != null && leftHand.PreviousHeldObject.CompareTag("Wand"))
            {
                state = State.Use;
            }

            if (rightHand.PreviousHeldObject != null && rightHand.PreviousHeldObject.CompareTag("Wand"))
            {
                state = State.Use;
            }
        }

        if (state == State.Use)
        {
            if (!useUI.activeSelf)
            {
                grabUI.SetActive(false);
                useUI.SetActive(true);
            }
            
            if (leftHand.PreviousHeldObject && InputBridge.Instance.LeftTriggerDown)
            {
                FinishTutorial();
            }
            if (rightHand.PreviousHeldObject && InputBridge.Instance.RightTriggerDown)
            {
                FinishTutorial();
            }
        }

        if (leftHand.PreviousHeldObject == null && rightHand.PreviousHeldObject == null && !grabUI.activeSelf)
        {
            state = State.Grab;
            grabUI.SetActive(true);
            useUI.SetActive(false);
        }
    }

    public void StartTutorial()
    {
        enabled = true;

        state = State.Grab;
        grabUI.SetActive(true);
        useUI.SetActive(false);
    }

    public void FinishTutorial()
    {
        grabUI.SetActive(false);
        useUI.SetActive(false);
        GetComponent<TutorialSequenceController>().FinishGrabTutorial();
        enabled = false;
    }
}