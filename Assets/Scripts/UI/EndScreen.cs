using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerWinText;
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;
    [SerializeField] private Canvas endCanvas;

    private int player1Score = 0;
    private int player2Score = 0;

    private void UpdateUI()
    {
        UpdateScoreText();
        UpdateWinText();
    }

    private void UpdateScoreText()
    {
        player1ScoreText.text = "PLAYER 1\nSCORE: " + player1Score;
        player2ScoreText.text = "PLAYER 2\nSCORE: " + player2Score;
    }

    private void UpdateWinText()
    {
        if (player1Score == player2Score)
        {
            playerWinText.text = "TIE";
        }
        else if (player1Score > player2Score)
        {
            playerWinText.text = "PLAYER 1 WINS";
        }
        else
        {
            playerWinText.text = "PLAYER 2 WINS";
        }
    }


    private void UpdateScores()
    {
        player1Score = ScoreManager.Instance.GetScoreFromIndex(0);
        player2Score = ScoreManager.Instance.GetScoreFromIndex(1);

    }

    public void ShowEndScreen()
    {
        UpdateScores();
        UpdateUI();
        endCanvas.enabled = true;
    }

}
