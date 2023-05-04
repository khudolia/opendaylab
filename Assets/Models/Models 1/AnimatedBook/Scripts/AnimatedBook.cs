using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guinealion.animatedBook;

public class AnimatedBook : MonoBehaviour
{
    bool isejected = false;

    public void ejectBook() {
            gameObject.GetComponent<Animation>().Play("EjectBook");
            isejected = true;
            gameObject.GetComponent<LightweightBookHelper>().Open(2.0f);

    }

    public void haulinBook(){
        gameObject.GetComponent<Animation>().Play("HaulInBook");
        isejected = false;
        gameObject.GetComponent<LightweightBookHelper>().Close(2.0f);
    }
}