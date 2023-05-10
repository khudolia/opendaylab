using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playVideo : MonoBehaviour
{
    public GameObject VideoPlayer;
    public int timeToStop;
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            VideoPlayer.SetActive(true);
            Destroy(VideoPlayer, timeToStop);

        }
    }
}
