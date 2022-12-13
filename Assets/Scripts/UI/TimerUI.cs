using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class TimerUI
{
    private TextMeshProUGUI timerText;
    // Start is called before the first frame update
    public TimerUI(TextMeshProUGUI timerText)
    {
        this.timerText = timerText;

    }

    public void UpdateTimerUI(int timeLeft)
    {
        timerText.text = timeLeft.ToString();
    }
}
