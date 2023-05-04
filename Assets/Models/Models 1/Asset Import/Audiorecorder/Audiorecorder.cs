using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Audiorecorder : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject playButton;
    public bool SolvePreviousRiddle;

    // Start is called before the first frame update
    void Start()
    {
        SolveRiddleWithSpawning();
        if(audioSource == null)
        {
            Debug.LogError("Audiosource not Found! Audiorecorder can't play clip.");
        }

    }

    private void SolveRiddleWithSpawning()
    {
        if (SolvePreviousRiddle)
        {
            LevelElementConfig config = gameObject.GetComponent<LevelElementConfig>();
            if(config != null)
            {
                config.RiddleSolved();
            }
        }
    }

    public void PlayPauseButton()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            if(playButton != null)
            {
                ButtonConfigHelper configHelper = playButton.GetComponent<ButtonConfigHelper>();
                if(configHelper != null)
                {
                    configHelper.MainLabelText = ">";
                }                
            }
        }
        else
        {
            audioSource.Play();
            if (playButton != null)
            {
                ButtonConfigHelper configHelper = playButton.GetComponent<ButtonConfigHelper>();
                if (configHelper != null)
                {
                    configHelper.MainLabelText = "||";
                }
            }
        }
    }
}
