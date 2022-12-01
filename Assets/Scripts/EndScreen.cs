using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerWinText;
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;
    public void ShowEndScreen()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
        player1ScoreText.text = "PLAYER 1\nSCORE: " + ScoreManager.Instance.Player1Score.ToString();
        player2ScoreText.text = "PLAYER 2\nSCORE: " + ScoreManager.Instance.Player2Score.ToString();
        if (ScoreManager.Instance.Player1Score == ScoreManager.Instance.Player2Score)
        {
            playerWinText.text = "TIE";
        }
        else if(ScoreManager.Instance.Player1Score > ScoreManager.Instance.Player2Score)
        {
            playerWinText.text = "PLAYER 1 WINS";
        }
        else
        {
            playerWinText.text = "PLAYER 2 WINS";
        }
    }

}
