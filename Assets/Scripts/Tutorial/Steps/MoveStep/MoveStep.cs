using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStep : MonoBehaviour
{
    public GameObject targetObject;

    public ArrowController arrow;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            arrow.Hide();
            FinishTutorial();
        }
    }

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