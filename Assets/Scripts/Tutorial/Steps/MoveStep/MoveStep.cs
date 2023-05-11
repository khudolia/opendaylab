using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStep : MonoBehaviour
{
    public GameObject walkUI;
    public GameObject walkArrowObject;
    public GameObject controllerTutorial;

    public GameObject targetObject;
    public ArrowController arrow;

    private void Start()
    {
        walkUI.SetActive(false);
        walkArrowObject.SetActive(false);
        controllerTutorial.SetActive(false);
        enabled = false;
    }

    public void StartTutorial()
    {
        targetObject.SetActive(true);
        walkUI.SetActive(true);
        walkArrowObject.SetActive(true);
        controllerTutorial.SetActive(true);
        enabled = true;
        arrow.Show();
    }

    public void FinishTutorial()
    {
        targetObject.SetActive(false);
        walkUI.SetActive(false);
        walkArrowObject.SetActive(false);
        controllerTutorial.SetActive(false);
        GetComponent<TutorialSequenceController>().FinishWalkTutorial();
        enabled = false;
    }
}