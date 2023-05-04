using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EingabePanelManager : MonoBehaviour
{
    public GameObject displayTextMeshObject;
    public string password;
    public Material materialPasswordCorrect;
    public Material materialPasswordIncorrect;
    public GameObject doorToOpen;

    private TMP_Text displayTextOutput;
    private string currentInput;
    private bool solved;
    private Color redColorDeactivated = new Color(150f/255f, 0f, 0f);
    private Color redColorHighlighted = new Color(255f/255f, 0f, 0f);
    private Color greenColorDeactivated = new Color(0f, 152f/255f, 39/255f);
    private Color greenColorHighlighted = new Color(0f, 255f/255f, 39/255f);
    private LevelElementConfig levelElementConfig;


    // Start is called before the first frame update
    void Start()
    {
        if(displayTextMeshObject != null)
        {
            displayTextOutput = displayTextMeshObject.GetComponent<TMP_Text>();
            displayTextOutput.text = "";
            currentInput = "";
            solved = false;
            password = password.ToUpper();
        }
        //Debug.Log("Colors: Red: " + redColorDeactivated.ToString() + " -> " + redColorHighlighted.ToString() 
        //    + "; Green: " + greenColorDeactivated.ToString() + " -> " + greenColorHighlighted.ToString());
    }

    public void SubmitLetter(string letter)
    {
        if (!solved)
        {
            currentInput += letter;
            UpdateDisplayText(currentInput);
            CheckInput();
        }
    }

    public void ClearInput()
    {
        if (!solved)
        {
            currentInput = "";
            UpdateDisplayText("");
        }
    }

    private void CheckInput()
    {
        if (InputCorrect())
        {
            ToggleGreenColor(materialPasswordCorrect, true);
            ToggleRedColor(materialPasswordIncorrect, false);
            solved = true;
            OpenDoor();
            levelElementConfig = gameObject.GetComponent<LevelElementConfig>();
            if (levelElementConfig != null)
            {
                levelElementConfig.RiddleSolved();
            }
        }
        else
        {
            ToggleRedColor(materialPasswordIncorrect, true);
            ToggleGreenColor(materialPasswordCorrect, false);
        }
    }

    private bool InputCorrect()
    {
        //Debug.Log("InputCorrect: currIn: " + currentInput + ", pw: " + password + ", Vergleich: " + currentInput.Equals(password).ToString());
        return currentInput.Equals(password);
    }

    private void UpdateDisplayText(string output)
    {
        displayTextOutput.text = output;
    }

    private void ToggleRedColor(Material redMaterial, bool highlight)
    {
        if(redMaterial != null)
        {
            if (highlight)
            {
                redMaterial.color =  redColorHighlighted;
            }else
            {
                redMaterial.color = redColorDeactivated;
            }
        }
    }

    private void ToggleGreenColor(Material greenMaterial, bool highlight)
    {
        if (greenMaterial != null)
        {
            if (highlight)
            {
                greenMaterial.color = greenColorHighlighted;
            }
            else
            {
                greenMaterial.color = greenColorDeactivated;
            }
        }
    }

    private void OpenDoor()
    {
        if (doorToOpen != null)
        {
            onDoorScript doorScript = doorToOpen.GetComponent<onDoorScript>();
            if (doorScript != null)
            {
                doorScript.handleDoor();
            }
        }
    }
}
