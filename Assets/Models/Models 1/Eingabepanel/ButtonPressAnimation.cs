using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressAnimation : MonoBehaviour
{
    private float originalScaleZ;
    private float minScale;
    private Vector3 currentScaleVector;
    private float step;
    public float SQUEEZING_PERCENTAGE = 50f;
    private bool animationStarted = false;
    private bool down = true;

    // Start is called before the first frame update
    void Start()
    {
        currentScaleVector = gameObject.transform.localScale;
        originalScaleZ = currentScaleVector.z;
        step = (originalScaleZ * (SQUEEZING_PERCENTAGE / 100f)) * 5f; // 5 for overall time of 0.4 (1/5 sec down, 1/5 sec up) second
        minScale = originalScaleZ - (originalScaleZ * (SQUEEZING_PERCENTAGE / 100f));
    }

    // Update is called once per frame
    void Update()
    {
        float newZValue;
        if (animationStarted && down && currentScaleVector.z > minScale)
        {
            newZValue = currentScaleVector.z - (step * Time.deltaTime);
            currentScaleVector.Set(currentScaleVector.x, currentScaleVector.y, newZValue);
            gameObject.transform.localScale = currentScaleVector;
        }
        else if(animationStarted && down && currentScaleVector.z <= minScale)
        {
            down = false;
            newZValue = currentScaleVector.z + (step * Time.deltaTime);
            currentScaleVector.Set(currentScaleVector.x, currentScaleVector.y, newZValue);
            gameObject.transform.localScale = currentScaleVector;
        }
        else if(animationStarted && !down && currentScaleVector.z < originalScaleZ)
        {
            newZValue = currentScaleVector.z + (step * Time.deltaTime);
            currentScaleVector.Set(currentScaleVector.x, currentScaleVector.y, newZValue);
            gameObject.transform.localScale = currentScaleVector;
        }
        else if(animationStarted && !down && currentScaleVector.z >= originalScaleZ)
        {
            newZValue = originalScaleZ;
            currentScaleVector.Set(currentScaleVector.x, currentScaleVector.y, newZValue);
            gameObject.transform.localScale = currentScaleVector;
            animationStarted = false;
            down = true;
        }
    }

    public void executeAnimation()
    {
        animationStarted = true;
    }
}
