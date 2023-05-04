using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawerConnector : MonoBehaviour
{
    public GameObject source;
    public GameObject target;
    public Vector3 offsetSource;
    public Vector3 offsetTarget;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        SetPositions();
    }

    // Update is called once per frame
    void Update()
    {
        SetPositions();
    }

    private void SetPositions()
    {
        lineRenderer.SetPosition(0, source.transform.position + offsetSource);
        lineRenderer.SetPosition(1, target.transform.position + offsetTarget);
    }
}
