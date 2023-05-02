using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStep : MonoBehaviour
{
    public GameObject targetObject;
    public ArrowController arrow;
    
 
    public void StartTutorial()
    {
        enabled = true;
        arrow.Show();
    }

    public void FinishTutorial()
    {
        GetComponent<TutorialSequenceController>().FinishWalkTutorial();
        enabled = false;
    }
}