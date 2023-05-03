using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePointerController : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    public GameObject explanationText;
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (explanationText != null && button != null)
        {
            _lineRenderer.SetPosition(0, explanationText.transform.position);
            _lineRenderer.SetPosition(1, button.transform.position);
        }
    }
}