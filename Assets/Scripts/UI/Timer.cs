using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    [SerializeField] int timeLeft = 60;
    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ticking());

    }

    private IEnumerator ticking()
    {
        while(timeLeft > 0)
        {
            if(!paused)
            {
                timeLeft -= 1;
                timerText.text = timeLeft.ToString();
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }
        }

        if (timeLeft <= 0)
            CanvasManager.Instance.ShowEndScreen();

    }

    public void stopClock()
    {
        paused = true;
    }

    public void startClock()
    {
        paused = false;
    }




}
