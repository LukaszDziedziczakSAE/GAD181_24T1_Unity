using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MatchTitleCard : MonoBehaviour
{
    [SerializeField] TMP_Text matchTitle;
    [SerializeField] RawImage matchSplashImage;
    [SerializeField] TMP_Text matchDescription;
    [SerializeField] float timeToShow;

    float timer;
    Minigames.Game miniGame;

    public void Initilize(Minigames.Game game)
    {
        miniGame = game;
        matchTitle.text = game.Name;
        matchDescription.text = game.Description;
        if (game.startTitleCardPicture != null) matchSplashImage.texture = game.startTitleCardPicture;

        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToShow)
        {
            //Game.Match.Mode = MinigameMatch.EState.inProgress;

            miniGame.Play();
            //gameObject.SetActive(false);
        }
    }
}
