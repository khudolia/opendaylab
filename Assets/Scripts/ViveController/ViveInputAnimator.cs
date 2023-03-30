using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class ViveInputAnimator : MonoBehaviour
{
    [Header("Objects")] public GameObject thumbPointer;
    public GameObject trigger;
    public GameObject sideButtonLeft;
    public GameObject sideButtonRight;
    public GameObject menuButton;
    public GameObject systemButton;

    [Header("Touchpad Settings")] public float maxRadius = 0.02f;
    public float maxHeight = 0.002f;
    public float maxLow = 0.001f;
    public float moveSpeed = 2.0f;

    private readonly float _maxRotation = -32.772f;
    private readonly Vector3 _maxPosition = new Vector3(0, -0.0263999999f, -0.0034f);

    private float _buttonInMove = -0.001f; //-0.0005f;

    void Update()
    {
        ShowTouchpadPoint();
        MoveTrigger();
        MoveButtons();
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
        float inputProgress = InputBridge.Instance.RightTrigger;

        var eulerAngles = trigger.transform.localEulerAngles;
        eulerAngles = new Vector3(_maxRotation * inputProgress, eulerAngles.y, eulerAngles.z);
        trigger.transform.localEulerAngles = eulerAngles;
        trigger.transform.localPosition = _maxPosition * inputProgress;
    }

    private void MoveButtons()
    {
        GripButton(InputBridge.Instance.RightGrip == 1);
        GripButton(InputBridge.Instance.RightGrip == 1);
        MoveButton(systemButton, InputBridge.Instance.BackButtonDown);
        MoveButton(menuButton, InputBridge.Instance.StartButton);
    }

    private void MoveButton(GameObject button, bool isPressed)
    {
        Vector3 localPosition = button.transform.localPosition;

        var isPressedPosition = isPressed ? _buttonInMove : 0f;
        localPosition = new Vector3(localPosition.z, isPressedPosition, localPosition.z);

        button.transform.localPosition = localPosition;
    }

    private void GripButton(bool isPressed)
    {
        Vector3 localPositionRight = sideButtonRight.transform.localPosition;
        Vector3 localPositionLeft = sideButtonLeft.transform.localPosition;

        var isPressedPosition = isPressed ? _buttonInMove : 0f;

        localPositionRight = new Vector3(-isPressedPosition, localPositionRight.y, localPositionRight.z);
        localPositionLeft = new Vector3(isPressedPosition, localPositionLeft.y, localPositionLeft.z);

        sideButtonRight.transform.localPosition = localPositionRight;
        sideButtonLeft.transform.localPosition = localPositionLeft;
    }
}