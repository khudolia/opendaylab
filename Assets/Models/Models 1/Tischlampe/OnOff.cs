using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OnOff : MonoBehaviour
{

    public GameObject pointLight;


    private bool on = true;


    public void ToggleLight()
    {
        //Debug.Log("ToggleLight");


        if (!on)
        {

            pointLight.SetActive(true);
            on = true;
        }
        else if (on)
        {
            pointLight.SetActive(false);
            on = false;
        }
    }
}