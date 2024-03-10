using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MatchEnd_CharacterListItem : MonoBehaviour
{
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text characterScore;

    public void Initilise(int playerNumber, int score)
    {
        if (playerNumber == 0)
        {
            characterName.text = Game.Player.PlayerName;
        }
        else
        {
            characterName.text = "Player " + (playerNumber + 1);
        }

        characterScore.text = score.ToString();
    }
}
