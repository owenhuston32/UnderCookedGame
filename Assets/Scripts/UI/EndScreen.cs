using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerWinText;
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;

    private int player1Score = 0;
    private int player2Score = 0;

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

    public void UpdateEndScreenUI()
    {
        UpdateScores();
        UpdateScoreText();
        UpdateWinText();
    }

    public void SetEndScreen(bool val)
    {
        if(val == true)
        {
            //update ui
            CanvasManager.Instance.ActiveMenu = StaticStrings.endMenu;
            UpdateEndScreenUI();
        }
        gameObject.GetComponent<Canvas>().enabled = val;
    }

    public void ReplayButtonPress()
    {
        SceneManager.LoadScene(0);
    }

}
