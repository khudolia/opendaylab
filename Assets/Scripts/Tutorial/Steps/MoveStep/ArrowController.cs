using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject targetObject;

    public float rotationSpeed = 5.0f;
    public float scaleSpeed = 1.0f;

    private bool isScaling = false;

    void Update()
    {
        // Rotate towards targetObject
        Vector3 direction = targetObject.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator ScaleCoroutine(float targetScale)
    {
        isScaling = true;
        float currentScale = transform.localScale.x;

        while (Mathf.Abs(currentScale - targetScale) > 0.01f)
        {
            currentScale = Mathf.Lerp(currentScale, targetScale, scaleSpeed * Time.deltaTime);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            yield return null;
        }

        transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        isScaling = false;
    }

    public void Show()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
        StartCoroutine(ScaleCoroutine(.2f));
    }

    public void Hide()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
        StartCoroutine(ScaleCoroutine(0.0f));
    }
}