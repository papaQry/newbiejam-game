using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeValue = 90f;
    [SerializeField] TextMeshProUGUI timeText;
    public bool timerRanOut = false;

    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }
        DisplayTime(timeValue);
        TimerAlertMode();
        NoTimeLeft();
    }

    public void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerAlertMode()
    {
        if(timeValue <= 30f)
        {
            animator.SetTrigger("onAlert");
            timeText.color = Color.red;
        }
    }

    void NoTimeLeft()
    {
        if(timeValue <= 0)
        {
            timerRanOut = true;
        }
    }
}
