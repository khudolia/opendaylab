using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLauncher : MonoBehaviour
{
    private TutorialSequenceController _tutorialSequenceController;

    private bool _isGoingOn = false;
    // Start is called before the first frame update
    void Start()
    {
        _tutorialSequenceController = GetComponent<TutorialSequenceController>();

        _isGoingOn = _tutorialSequenceController.state != TutorialState.None;
    }

    public void ChangeState()
    {
        if(_isGoingOn)
            _tutorialSequenceController.CancelTutorial();
        else
            _tutorialSequenceController.StartTutorial();

        _isGoingOn = !_isGoingOn;
    }

    public void StartTutorialOnlyOnce()
    {
        if(!_isGoingOn)
            _tutorialSequenceController.StartTutorial();

        _isGoingOn = true;
    }
}
