using System.Collections;
using UnityEngine;

public enum CurrentPart
{
    Trigger,
    Side,
    Touchpad,
    Menu,
    System
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
    public CurrentPart part;
    public Material material1;
    public Material material2;
    
    private CurrentPart _previousActivePart;
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
