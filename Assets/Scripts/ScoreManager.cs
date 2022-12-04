using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField] TextMeshProUGUI player1ScoreText;
    private int player1Score = 0;

    [SerializeField] TextMeshProUGUI player2ScoreText;
    private int player2Score = 0;

    public int Player1Score { get => player1Score; }
    public int Player2Score { get => player2Score; }

    public void AddScore(GameObject player, GameObject food)
    {
        if(player.CompareTag("Player1"))
        {
            player1Score += food.GetComponent<Cook>().CurrentScore();
            player1ScoreText.text = "Score: " + player1Score;
        }
        else
        {
            player2Score += food.GetComponent<Cook>().CurrentScore();
            player2ScoreText.text = "Score: " + player2Score;
        }
    }


}
