using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    // SerializableField allows you to set the variables in unity inspector

    [SerializeField]
    private TMP_Text textTimer;

    [SerializeField]
    private float timeDuration = 30f * 60f;
    [SerializeField]
    private float flashInterval = 1f;
    [SerializeField]
    private float flashDuration = 4f;
    [SerializeField]
    private float lastMinutes = 5f * 60f;

    private float timer;
    private float flashTimer;

    private AudioSource source;

    private Color32 black = new Color32(0, 4, 250, 255);
    private Color32 red = new Color32(255, 0, 0, 255);

    private bool running;

    // variables to send an event at the end
    public delegate void TimerFinishedEvent();
    public static event TimerFinishedEvent TimerFinished;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        ResetTimer();
        textTimer.color = black;
        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            // flash timer when half of the game time is over or the last minutes are on
            if((timer <= timeDuration / 2 && timer >= timeDuration / 2 - flashDuration) || 
               (timer <= lastMinutes && timer >= lastMinutes - flashDuration)) {

                // starts Audiosource
                if (!source.isPlaying)
                {
                   source.Play(); 
                }

                this.Flash();
                
            }

            // stops Audiosource
            if ((timer <= timeDuration / 2 - flashDuration && source.isPlaying && timer >= lastMinutes) || 
                (timer <= lastMinutes - flashDuration && source.isPlaying))
            {
                source.Stop();
            }

            // chnage color to red in the last minutes
            if(timer <= lastMinutes) {
                textTimer.color = red;
            }
            UpdateTimerDisplay(timer);
        } else
        {
            if (running)
            {
                // Trigger Timer finished event once at the end of the Timer.
                TimerFinished();
                running = false;
            }

            // flash when time reaches 0
            this.EndFlash();

             // starts Audiosource when time reaches 0 and decreases volume
            if (!source.isPlaying)
            {
                source.Play();
                source.volume = 0.2f;
            }
        }
        
    }

    // sets timer to the given timeDuration
    private void ResetTimer()
    {
        timer = timeDuration;

    }
    
    // updates minutes and seconds in the UI
    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        textTimer.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }

    // enables or disables the text based on the given flashInterval
    private void Flash()
    {
        if (flashTimer <= 0) {
            flashTimer = flashInterval;
        } else if (flashTimer >= flashInterval / 2) {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        } else {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
        }
    }

    private void EndFlash() 
    {
        // sets timer to zero, to prevent negative timer
        if (timer != 0) {
            timer = 0;
            UpdateTimerDisplay(timer);
        }

        this.Flash();
    }

    // enbales and disables text for flash effect
    private void SetTextDisplay(bool enabled)
    {
        textTimer.enabled = enabled;
    }

    public float GetCurrentTime()
    {
        return timer;
    }

    public string GetLapsedTimeStr()
    {
        float lapsedTime = timeDuration - timer;
        float minutes = Mathf.FloorToInt(lapsedTime / 60);
        float seconds = Mathf.FloorToInt(lapsedTime % 60);
        return string.Format("{00:00}:{1:00}", minutes, seconds);
    }
}
