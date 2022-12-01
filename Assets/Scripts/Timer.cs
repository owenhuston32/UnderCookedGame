using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] EndScreen endScreenScript;
    TextMeshProUGUI timerText;
    [SerializeField] int timeLeft = 60;

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
            timeLeft -= 1;
            timerText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1);
        }

        endScreenScript.ShowEndScreen();

    }



}
