using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurController : MonoBehaviour
{
    public float smoothnessMax = 10f; // The maximum smoothness value to animate to
    public float smoothnessMin = .515f; // The maximum smoothness value to animate to
    public float duration = 2f; // The duration of the animation in seconds
    public bool reverseDirection = false; // Whether to animate from max to start or start to max

    private Material material; // The material to modify
    private float smoothnessStart; // The initial smoothness value
    private float smoothnessTarget; // The target smoothness value

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        
    }

    private IEnumerator AnimateSmoothness()
    {
        float t = 0f;
        while (t < duration)
        {
            // Calculate the progress of the animation from 0 to 1
            float progress = t / duration;

            // Interpolate the smoothness value from the start value to the target value
            float smoothness = Mathf.Lerp(smoothnessStart, smoothnessTarget, progress);

            // Set the smoothness value on the material
            material.SetFloat("_Smoothness", smoothness);

            // Wait for the next frame
            yield return null;

            // Update the current time in the animation
            t += Time.deltaTime;
        }

        // Set the final smoothness value to the target value
        material.SetFloat("_Smoothness", smoothnessTarget);
        if(!reverseDirection)
            gameObject.SetActive(false);

        // If the direction is reversed, swap the start and target values and animate again
        /*if (reverseDirection)
        {
            (smoothnessStart, smoothnessTarget) = (smoothnessTarget, smoothnessStart);
            StartCoroutine(AnimateSmoothness());
        }*/
    }

    private void StartAnim()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        smoothnessStart = material.GetFloat("_Smoothness");
       
        smoothnessTarget = reverseDirection ? smoothnessMin : smoothnessMax;
        StartCoroutine(AnimateSmoothness());
    }
    
    public void FadeIn()
    {
        gameObject.SetActive(true);
        reverseDirection = true;
        StartAnim();
    }

    public void FadeOut()
    {
        reverseDirection = false;
        StartAnim();
    }
}
