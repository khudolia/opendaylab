using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStep : MonoBehaviour
{
    public GameObject targetObject;
    public ArrowController arrow;
    
 
    public void StartTutorial()
    {
        targetObject.SetActive(true);
        enabled = true;
        arrow.Show();
    }

    public void FinishTutorial()
    {
        targetObject.SetActive(false);
        GetComponent<TutorialSequenceController>().FinishWalkTutorial();
        enabled = false;
    }
}