using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] int playerNum = 0;
    [SerializeField] TextMeshProUGUI playerScoreText;

    public void updateScore()
    {
        playerScoreText.text = "SCORE: " + ScoreManager.Instance.GetScoreFromIndex(playerNum);
    }

}
