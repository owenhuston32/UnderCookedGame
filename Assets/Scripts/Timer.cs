using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class Timer : MonoBehaviour
{
    private Action<int> TimerTickDownAction = null;
    private TimerUI timerUI = null;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] int initialTime = 60;
    private int timeLeft;
    public UnityEvent TimesUpEvent;
    private bool paused = false;


    private void Start()
    {

        if (timerText != null && timerUI == null)
        {
            timerUI = new TimerUI(timerText);
            TimerTickDownAction = timerUI.UpdateTimerUI;
        }
        ResetTimer();
    }

    public void ResetTimer()
    {
        timeLeft = initialTime;
    }

    public void StartTimer()
    {
        StartCoroutine(ticking());
    }

    private IEnumerator ticking()
    {
        while(timeLeft > 0)
        {
            if(!paused)
            {
                timeLeft -= 1;
                if(TimerTickDownAction != null)
                    TimerTickDownAction(timeLeft);
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }
        }

        if (timeLeft <= 0)
            TimesUpEvent.Invoke();


        // reset at the end
        ResetTimer();
    }

    public void unpauseClock()
    {
        paused = true;
    }

    public void pauseClock()
    {
        paused = false;
    }




}
