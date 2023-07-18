using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoButtonController : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public float animationDuration = 1.0f;
    public VideoPlayer VideoPlayer;
    private bool isAnimating = false;

    private Vector3 originalScale1;
    private Vector3 originalScale2;

    public float delay = 1;
    private float _delay = 1;

    private bool isActive = false;
    
    private void Start()
    {
        originalScale1 = object1.transform.localScale;
        originalScale2 = object2.transform.localScale;
        
        VideoPlayer.Pause();
    }

    private void Update()
    {
        _delay -= Time.deltaTime;
        _delay = Mathf.Clamp(_delay, .0f, Single.MaxValue);
    }

    public void ShowPlay()
    {
        StartCoroutine(AnimateObjectVisibility(true, object1, originalScale1));
        StartCoroutine(AnimateObjectVisibility(false, object2, originalScale2));
    }

    public void ShowPause()
    {
        StartCoroutine(AnimateObjectVisibility(false, object1, originalScale1));
        StartCoroutine(AnimateObjectVisibility(true, object2, originalScale2));
    }

    private IEnumerator AnimateObjectVisibility(bool isEnable, GameObject targetObject, Vector3 originalScale)
    {
        Vector3 startScale = targetObject.transform.localScale;
        Vector3 targetScale = !isEnable ? Vector3.zero : originalScale;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            targetObject.transform.localScale =
                Vector3.Lerp(startScale, targetScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object is set to the target scale after the animation (avoids floating-point errors)
        targetObject.transform.localScale = targetScale;

        // Toggle the object's visibility after the animation is completed
        targetObject.SetActive(isEnable);

        isAnimating = false;
        _delay = delay;
    }
}
