using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SuspectInputConfig : MonoBehaviour
{

    public GameObject templateButton;
    public GameObject buttonListParent;
    public GameObject dialogPrefab;
    public float MENU_WIDTH = 1.2f;
    public int SUSPECTS_PER_ROW = 4;
    public float START_HEIGHT = 1.0f;
    public float DISTANCE_BETWEEN_ROWS = 0.25f;
    public float MAX_ROTATION = 30f;
    public GameObject Timer;
    [NonReorderable]
    public Suspect[] suspectList;

    private Dictionary<string, Suspect> suspectDict = new Dictionary<string, Suspect>();
    private List<GameObject> suspectButtons = new List<GameObject>();
    private float menuDepth;
    private Suspect currentSuspectOfPlayer;


    private void Awake()
    {
        menuDepth = 0; // MENU_WIDTH * 0.15f;
        CreateSuspectButtons();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void CreateSuspectButtons()
    {
        if (templateButton != null && buttonListParent != null)
        {
            try
            {
                foreach (Suspect suspect in suspectList)
                {
                    GameObject newButton = Instantiate(templateButton, buttonListParent.transform);
                    if (newButton != null)
                    {
                        newButton.SetActive(true);
                        Transform textObject = newButton.transform.Find("IconAndText");
                        TMP_Text tmpObject = textObject.GetComponentInChildren<TMP_Text>();
                        tmpObject.text = suspect.name;
                        suspectButtons.Add(newButton);
                        suspectDict.Add(suspect.name, suspect);
                    }
                }
                SetButtonPositions();
            }
            catch (Exception e)
            {
                Debug.LogError("Suspect list could not be created becaues a Exception occured: " + e.Message + ", \n" + e.StackTrace);
            }

        }
        else
        {
            Debug.LogError("Suspect list can not be created because the template button or the buttonListParent is missing!");
        }
    }

    private void SetButtonPositions()
    {
        float currentRotationY;

        float maxDistanceFromCenterX = MENU_WIDTH / 2f;
        float distanceBetweenButtons = maxDistanceFromCenterX / ((float)(SUSPECTS_PER_ROW - 1) / 2f);
        float currentDistanceX = -maxDistanceFromCenterX;
        float currentDistanceZ = -menuDepth;
        float currentDistanceY = START_HEIGHT;
        float aspectRatio = maxDistanceFromCenterX / menuDepth; // r = l/h   ->  h = l/r

        foreach(GameObject button in suspectButtons)
        {
            // rotation:
            float percentualDistance = currentDistanceX / maxDistanceFromCenterX;
            currentRotationY = percentualDistance * MAX_ROTATION;
            button.transform.Rotate(new Vector3(0, currentRotationY, 0));

            // position translation:
            button.transform.position = new Vector3(button.transform.position.x + currentDistanceX,
                button.transform.position.y + currentDistanceY, button.transform.position.z + currentDistanceZ);

            // Adjust translation parameters for next button
            if (currentDistanceX >= maxDistanceFromCenterX)
            {   
                // Start new row
                currentDistanceX = -maxDistanceFromCenterX;
                currentDistanceY -= DISTANCE_BETWEEN_ROWS;
                currentDistanceZ = -menuDepth;
            }else
            {
                currentDistanceX += distanceBetweenButtons;
                if(currentDistanceX == 0)
                {
                    currentDistanceZ = 0.02f * menuDepth;
                }
                else
                {
                    currentDistanceZ = -Math.Abs(currentDistanceX) / aspectRatio; // h = l/r
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AccuseSuspect(TMP_Text tmpWithSuspectName)
    {
        if(tmpWithSuspectName != null)
        {
            string suspectName = tmpWithSuspectName.text;
            //Debug.Log("Accuse: " + tmpWithSuspectName.text);
            Dialog dialog = Dialog.Open(dialogPrefab, DialogButtonType.Yes | DialogButtonType.Cancel,"Bestätigung der Täterauswahl", "Möchten Sie " + suspectName + " wirklich als Täter*in beschuldigen?", true);
            dialog.OnClosed += OnDialogClosed;
            suspectDict.TryGetValue(suspectName, out currentSuspectOfPlayer);
            if(currentSuspectOfPlayer == null)
            {
                Debug.LogError("No suspect object could be found in the suspect dictionary for the given name.");
            }
        }
        else
        {
            Debug.LogError("A Suspect list button was pressed, but the given TMP was null!");
        }
        
    }

    private void OnDialogClosed(DialogResult dialogResult)
    {
        if(dialogResult != null && dialogResult.Result == DialogButtonType.Yes)
        {
            if(currentSuspectOfPlayer != null && currentSuspectOfPlayer.murderer)
            {
                ActionsOnMurdererIdentified();
            }
            else
            {
                ActionsOnWrongSuspectIdentification();
            }
        }
    }

    private void ActionsOnMurdererIdentified()
    {
        // TODO Attention. Timer Calculation does not work because the given Timer Object gets newly instanciated on the marker recognition.
        // New concept for Timer calculation needed! Best practice would be to ask the LevelContentManager for the timer object.
        string lapsedTime = "??:??";
        if(Timer != null)
        {
            TimerScript timerScript = Timer.GetComponent<TimerScript>();
            lapsedTime = timerScript.GetLapsedTimeStr();
        }
        Debug.Log("Congrats!! You identified the murderer! Well done!");
        Dialog dialog = Dialog.Open(dialogPrefab, DialogButtonType.OK, "Glückwunsch",
            "Herzlichen Glückwunsch Sie haben den Täter in " + lapsedTime + " erfolgreich identifiziert! Wir bedanken uns für Ihre Mithilfe! \n Sie kehren nun zum Hauptmenü zurück.", true);
        dialog.OnClosed += OnLevelFinishedSuccesfullyDialogClosed;

    }

    private void OnLevelFinishedSuccesfullyDialogClosed(DialogResult dialogResult)
    {
        SwitchScene switchSceneScript = gameObject.GetComponent<SwitchScene>();
        switchSceneScript.SwitchToScene();
    }


    private void ActionsOnWrongSuspectIdentification()
    {
        Dialog dialog = Dialog.Open(dialogPrefab, DialogButtonType.OK, "Falsche*r Täter*in",
            "Ihre Auswahl war leider falsch! Versuchen Sie es erneut!", true);
        Debug.Log("Sorry, but " + currentSuspectOfPlayer.name + " wasn't the murderer! Maybe have a closer look at the evidence! Good Luck next time!");
    }


    // Inner private class

    [System.Serializable]
    public class Suspect
    {
        public string name;
        public bool murderer;
    }
}
