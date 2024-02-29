using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MatchStatus : MonoBehaviour
{
    [SerializeField] TMP_Text matchTimer;
    [SerializeField] TMP_Text player1Name;
    [SerializeField] TMP_Text player1Score;
    [SerializeField] TMP_Text player2Name;
    [SerializeField] TMP_Text player2Score;
    [SerializeField] TMP_Text player3Name;
    [SerializeField] TMP_Text player3Score;
    [SerializeField] TMP_Text player4Name;
    [SerializeField] TMP_Text player4Score;
    [SerializeField] Button pauseButton;


    private void OnEnable()
    {
        pauseButton.onClick.AddListener(OnPauseButtonPress);

        if (Game.Match.Result == null)
        {
            Debug.LogError(name + ": missing Game.Match.Result referance");
            return;
        }

        if (Game.Match.Result.Scores == null)
        {
            Debug.LogError(name + ": missing Game.Match.Result.Results referance");
            return;
        }

        InitiliseStatus();
    }

    private void OnDisable()
    {
        pauseButton.onClick.RemoveListener(OnPauseButtonPress);
    }

    private void Update()
    {
        matchTimer.text = Game.Match.MatchTime.ToString("F0");

        /*if (Game.Match == null)
        {
            Debug.LogError(name + ": missing Game.Match referance");
            return;
        }
        else Debug.LogWarning(name + ": found Game.Match referance");

        if (Game.Match.Result == null)
        {
            Debug.LogError(name + ": missing Game.Match.Result referance");
            return;
        }
        else Debug.LogWarning(name + ": found Game.Match.Result referance");

        if (Game.Match.Result.Scores == null)
        {
            Debug.LogError(name + ": missing Game.Match.Result.Scores referance");
        }
        else Debug.LogWarning(name + ": found Game.Match.Result.Scores referance");*/
    }

    private void OnPauseButtonPress()
    {

    }

    public void InitiliseStatus()
    {
        print("Initilizing MatchStatus for " + Game.Match.Result.Scores.Count + " players");

        if (Game.Match.Result.Scores.ContainsKey(0))
        {
            player1Name.gameObject.SetActive(true);
            player1Score.gameObject.SetActive(true);

            player1Name.text = Game.Player.PlayerName;
            player1Score.text = 0.ToString();
        }
        else
        {
            player3Name.gameObject.SetActive(false);
            player3Score.gameObject.SetActive(false);
        }

        if (Game.Match.Result.Scores.ContainsKey(1))
        {
            player2Name.gameObject.SetActive(true);
            player2Score.gameObject.SetActive(true);

            player2Name.text = "Player 2";
            player2Score.text = 0.ToString();
        }
        else
        {
            player2Name.gameObject.SetActive(false);
            player2Score.gameObject.SetActive(false);
        }

        if (Game.Match.Result.Scores.ContainsKey(2))
        {
            player3Name.gameObject.SetActive(true);
            player3Score.gameObject.SetActive(true);

            player3Name.text = "Player 3";
            player3Score.text = 0.ToString();
        }
        else
        {
            player3Name.gameObject.SetActive(false);
            player3Score.gameObject.SetActive(false);
        }

        if (Game.Match.Result.Scores.ContainsKey(3))
        {
            player4Name.gameObject.SetActive(true);
            player4Score.gameObject.SetActive(true);

            player4Name.text = "Player 4";
            player4Score.text = 0.ToString();
        }
        else
        {
            player4Name.gameObject.SetActive(false);
            player4Score.gameObject.SetActive(false);
        }
    }

    public void UpdateStatus()
    {
        if (player1Score.gameObject.activeSelf)
        {
            player1Score.text = Game.Match.Result.Scores[0].ToString();
        }

        if (player2Score.gameObject.activeSelf)
        {
            player2Score.text = Game.Match.Result.Scores[1].ToString();
        }

        if (player3Score.gameObject.activeSelf)
        {
            player3Score.text = Game.Match.Result.Scores[2].ToString();
        }

        if (player4Score.gameObject.activeSelf)
        {
            player4Score.text = Game.Match.Result.Scores[3].ToString();
        }
    }
}
