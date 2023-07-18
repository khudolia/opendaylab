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
    
    // Call this method to trigger the visibility change with animation
    public void ToggleObjectsVisibility()
    {
        if (isAnimating && _delay != 0)
            return;

        isAnimating = true;
        isActive = !isActive;
        
        StartCoroutine(AnimateObjectVisibility(object1, originalScale1));
        StartCoroutine(AnimateObjectVisibility(object2, originalScale2));
        
        if(isActive)
            VideoPlayer.Play();
        else
            VideoPlayer.Pause();

    }

    private IEnumerator AnimateObjectVisibility(GameObject targetObject, Vector3 originalScale)
    {
        Vector3 startScale = targetObject.transform.localScale;
        Vector3 targetScale = targetObject.activeInHierarchy ? Vector3.zero : originalScale;
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
        targetObject.SetActive(!targetObject.activeInHierarchy);

        isAnimating = false;
        _delay = delay;
    }
}