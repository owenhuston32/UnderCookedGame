using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] Timer gameTimer;
    public void StartGame()
    {
        ScoreManager.Instance.ResetScores();
        GetComponent<Canvas>().enabled = true;
        gameTimer.StartTimer();
    }


}
