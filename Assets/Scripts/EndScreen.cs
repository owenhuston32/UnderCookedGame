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
    public void ShowEndScreen()
    {
        endCanvas.enabled = true;

        int player1Score = ScoreManager.Instance.Player1Score;
        int player2Score = ScoreManager.Instance.Player2Score;


        player1ScoreText.text = "PLAYER 1\nSCORE: " + player1Score;
        player2ScoreText.text = "PLAYER 2\nSCORE: " + player2Score;
        if (player1Score == player2Score)
        {
            playerWinText.text = "TIE";
        }
        else if(player1Score > player2Score)
        {
            playerWinText.text = "PLAYER 1 WINS";
        }
        else
        {
            playerWinText.text = "PLAYER 2 WINS";
        }
    }

}
