using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private int[] scores = new int[2];

    public UnityEvent UpdatePlayer1ScoreUI;
    public UnityEvent UpdatePlayer2ScoreUI;



    public void AddScore(int playerIndex, GameObject food)
    {

        if (playerIndex == 0)
        {
            scores[0] += food.GetComponent<Cook>().CurrentScore();
            UpdatePlayer1ScoreUI.Invoke();
        }
        else
        {
            scores[1] += food.GetComponent<Cook>().CurrentScore();
            UpdatePlayer2ScoreUI.Invoke();
        }
    }

    public int GetScoreFromIndex(int index)
    {
        return scores[index];
    }

}
