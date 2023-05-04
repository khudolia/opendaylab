using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDoorScript : MonoBehaviour
{
    bool isopen = false;

    public void handleDoor() {
        if (isopen) {
            gameObject.GetComponent<Animation>().Play("close_door");
            isopen = false;
        } else {
            gameObject.GetComponent<Animation>().Play("open_door");
            isopen = true;
        }
    }
}
