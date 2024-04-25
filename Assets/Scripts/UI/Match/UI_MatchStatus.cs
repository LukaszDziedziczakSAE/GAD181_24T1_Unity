using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MatchStatus : MonoBehaviour
{
    [SerializeField] TMP_Text matchTimer;
    [SerializeField] Button pauseButton;
    [SerializeField] UI_MatchStatusPlayerBox playerBox1;
    [SerializeField] UI_MatchStatusPlayerBox playerBox2;
    [SerializeField] UI_MatchStatusPlayerBox playerBox3;
    [SerializeField] UI_MatchStatusPlayerBox playerBox4;
    [SerializeField] UIMover mover;
    [SerializeField] bool showCountdown;//countup

    private void OnEnable()
    {
        if (mover != null)
        {
            mover.SetOffScreenPosition();
            mover.MoveToOnScreen();
        }

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
        if (showCountdown)
            matchTimer.text = ((ArrowShootingMatch)Game.Match).MatchTimeRemaining.ToString("F0");
        else 
            matchTimer.text = Game.Match.MatchTime.ToString("F0");
    }

    private void OnPauseButtonPress()
    {
        if (Game.UI == null)
        {
            Debug.LogError("Missing UI referance");
            return;
        }

        if (Game.UI.PauseMenu == null)
        {
            Debug.LogError("Missing UI Pause Menu referance");
            return;
        }

        Game.UI.PauseMenu.gameObject.SetActive(true);
    }

    public void InitiliseStatus()
    {
        print("Initilizing MatchStatus for " + Game.Match.Result.Scores.Count + " players");

        if (Game.Match.Result.Scores.ContainsKey(0))
        {
            playerBox1.gameObject.SetActive(true);
            playerBox1.Initilise(0);
        }
        else playerBox1.gameObject.SetActive(false);

        if (Game.Match.Result.Scores.ContainsKey(1))
        {
            playerBox2.gameObject.SetActive(true);
            playerBox2.Initilise(1);
        }
        else playerBox2.gameObject.SetActive(false);

        if (Game.Match.Result.Scores.ContainsKey(2))
        {
            playerBox3.gameObject.SetActive(true);
            playerBox3.Initilise(2);
        }
        else playerBox3.gameObject.SetActive(false);

        if (Game.Match.Result.Scores.ContainsKey(3))
        {
            playerBox4.gameObject.SetActive(true);
            playerBox4.Initilise(3);
        }
        else playerBox4.gameObject.SetActive(false);
    }

    public void UpdateStatus()
    {
        /*if (player1Score.gameObject.activeSelf)
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
        }*/

        if (playerBox1.gameObject.activeSelf) playerBox1.UpdatePlayerScore();
        if (playerBox2.gameObject.activeSelf) playerBox2.UpdatePlayerScore();
        if (playerBox3.gameObject.activeSelf) playerBox3.UpdatePlayerScore();
        if (playerBox4.gameObject.activeSelf) playerBox4.UpdatePlayerScore();
    }
}
