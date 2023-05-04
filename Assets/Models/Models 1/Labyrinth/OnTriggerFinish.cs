using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerFinish : MonoBehaviour
{
    Vector3 originalPos;

    [SerializeField]
    private GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        if(ball) {
            // save start position of the ball
            originalPos = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);
            Debug.Log(ball.name);
        }
        //alternatively, just: originalPos = gameObject.transform.position;
 
    }

    void OnTriggerEnter(Collider other)
    {
      if(ball && other.name == "Kugel") {
        Debug.Log("Finished");
        // set position of the ball to the original position
        ball.transform.position = originalPos;
      }
  }
}