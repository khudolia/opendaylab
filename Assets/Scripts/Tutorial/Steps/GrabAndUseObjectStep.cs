using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class GrabAndUseObjectStep : MonoBehaviour
{
    enum State
    {
        None,
        Grab,
        Use
    }

    public HandController leftHand;
    public HandController rightHand;

    public GameObject grabUI;
    public GameObject useUI;

    private State _state = State.None;

    private void Start()
    {
        grabUI.SetActive(false);
        useUI.SetActive(false);
    }

    void Update()
    {
        if (_state == State.Grab)
        {
            if (leftHand.PreviousHeldObject != null && leftHand.PreviousHeldObject.CompareTag("Wand"))
            {
                _state = State.Use;
            }

            if (rightHand.PreviousHeldObject != null && rightHand.PreviousHeldObject.CompareTag("Wand"))
            {
                _state = State.Use;
            }
        }

        if (_state == State.Use)
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
            _state = State.Grab;
            grabUI.SetActive(true);
            useUI.SetActive(false);
        }
    }

    public void StartTutorial()
    {
        _state = State.Grab;
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