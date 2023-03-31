using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialState
{
    Walk,
    GrabObject,
    UseObject,
    Menu,
}

public class TutorialController : MonoBehaviour
{
    public TutorialState state;
    private TutorialState _previousState;
    
    void Update()
    {
        if (state != _previousState)
        {
            switch (state)
            {
                case TutorialState.Menu:
                    StartMenuTutorial();
                    break;
                case TutorialState.Walk:
                   // ActivateObject(sideButtonLeft);
                    break;
                case TutorialState.GrabObject:
                   // ActivateObject(systemButton);
                    break;
                case TutorialState.UseObject:
                   // ActivateObject(touchpad);
                    break;

            }

            
            _previousState = state;
        }
    }

    private void StartMenuTutorial()
    {
        
    }
}
