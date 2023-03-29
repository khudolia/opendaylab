using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class ViveInputAnimator : MonoBehaviour
{
    [Header("Objects")] public GameObject thumbPointer;
    public GameObject trigger;

    [Header("Touchpad Settings")]
    public float maxRadius = 0.02f;
    public float maxHeight = 0.002f;
    public float maxLow = 0.001f;
    public float moveSpeed = 2.0f;

    [Header("Trigger Settings")]
    public float maxRotation = -32.772f;
    public Vector3 maxPosition = new Vector3(0,-0.0263999999f,-0.00490000006f);


    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        ShowTouchpadPoint();
    }

    private void ShowTouchpadPoint()
    {
        Vector2 inputDirection = InputBridge.Instance.RightThumbstickAxis;

        var position = thumbPointer.transform.localPosition;
        Vector3 newPosition = new Vector3(-inputDirection.x * maxRadius,
            inputDirection.y > .0f ? inputDirection.y * maxHeight : inputDirection.y * maxLow,
            -inputDirection.y * maxRadius);
        
        position = Vector3.MoveTowards(position, newPosition, moveSpeed * Time.deltaTime);
        thumbPointer.transform.localPosition = position;
    }

    private void MoveTrigger()
    {
        float inputDirection = InputBridge.Instance.RightTrigger;

        var position = thumbPointer.transform.localPosition;
       //trigger.transform.rotation = 

        //Debug.Log(inputDirection + " " + newPosition);

        //position = Vector3.MoveTowards(position, newPosition, moveSpeed * Time.deltaTime);
        trigger.transform.localPosition = position;
    }
}