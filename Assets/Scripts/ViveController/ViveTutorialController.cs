using System.Collections;
using UnityEngine;

public enum CurrentPart
{
    Trigger,
    Side,
    Touchpad,
    Menu,
    System,
    None
}

public class ViveTutorialController : MonoBehaviour
{
    
    [Header("Objects")] public GameObject touchpad;
    public GameObject trigger;
    public GameObject sideButtonLeft;
    public GameObject sideButtonRight;
    public GameObject menuButton;
    public GameObject systemButton;

    
    [Header("Settings")] 
    public CurrentPart part = CurrentPart.None;
    public Material material1;
    public Material material2;
    
    private CurrentPart _previousActivePart = CurrentPart.None;
    private GameObject _previousActiveObject;
    private float duration = 1f;
    
    void Update()
    {
        if (part != _previousActivePart)
        {
            DeactivateObject();

            switch (part)
            {
                case CurrentPart.Menu:
                    ActivateObject(menuButton);
                    break;
                case CurrentPart.Side:
                    ActivateObject(sideButtonLeft);
                    ActivateObject(sideButtonRight);
                    break;
                case CurrentPart.System:
                    ActivateObject(systemButton);
                    break;
                case CurrentPart.Touchpad:
                    ActivateObject(touchpad);
                    break;
                case CurrentPart.Trigger:
                    ActivateObject(trigger);
                    break;

            }

            
            _previousActivePart = part;
        }
    }

    private void ActivateObject(GameObject part)
    {
        _previousActiveObject = part;
        StartCoroutine(ChangeMaterial(part, true));
    }

    private void DeactivateObject()
    {
        if(_previousActivePart == CurrentPart.Side)
            StartCoroutine(ChangeMaterial(sideButtonLeft, false));

        if(_previousActiveObject != null)
            StartCoroutine(ChangeMaterial(_previousActiveObject, false));
    }
    
    IEnumerator ChangeMaterial(GameObject part, bool isActive)
    {
        Renderer renderer = part.GetComponent<Renderer>();
        Material oldMaterial = renderer.material;
        float startTime = Time.time;
        Material newMaterial = isActive ? material2 : material1;
        while (Time.time - startTime < duration)
        {
            renderer.material.Lerp(oldMaterial, newMaterial, (Time.time - startTime) / duration);
            yield return null;
        }

        renderer.material = newMaterial;
    }
}
