using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MatchStatusPlayerBox : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playerScore;

    int playerNumber;

    public void Initilise(int playerNumber)
    {
        this.playerNumber = playerNumber;

        UpdatePlayerName();
    }

    private void UpdatePlayerName()
    {
        if (playerNumber == 0)
        {
            playerName.text = Game.Player.PlayerName;
        }
        else
        {
            playerName.text = "Player " + (playerNumber + 1).ToString();
        }

        playerScore.text = 0.ToString();
    }

    public void UpdatePlayerScore()
    {
        playerScore.text = Game.Match.Result.Scores[playerNumber].ToString();
    }
}
