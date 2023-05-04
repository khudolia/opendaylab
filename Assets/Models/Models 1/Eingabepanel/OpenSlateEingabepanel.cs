using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSlateEingabepanel : MonoBehaviour
{
    public GameObject slate;
    private Vector3 orignalSlatePosition;

    // Start is called before the first frame update
    void Awake()
    {
        if(slate != null)
        {
            orignalSlatePosition = new Vector3(slate.transform.position.x, slate.transform.position.y, slate.transform.position.z);
        }
    }

    private void Start()
    {
        slate.transform.position = orignalSlatePosition;
    }

    public void OpenInputSlate()
    {
        if(slate != null)
        {
            slate.SetActive(true);
        }
    }
}
