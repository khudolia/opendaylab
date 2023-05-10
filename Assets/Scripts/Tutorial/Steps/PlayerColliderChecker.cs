using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderChecker : MonoBehaviour
{
    public GameObject player;
    public ArrowController arrow;

    public MoveStep moveStep;
    public float delay = 1;
    private float _delay = 1;

    private void Update()
    {
        _delay -= Time.deltaTime;
        _delay = Mathf.Clamp(_delay, .0f, Single.MaxValue );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            moveStep.FinishTutorial();
        }
        
        if (other.gameObject == arrow.gameObject && _delay == 0)
        {
            _delay = delay;

            arrow.Hide();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == arrow.gameObject && _delay == 0)
        {
            _delay = delay;
            arrow.Show();
        }
    }
}
