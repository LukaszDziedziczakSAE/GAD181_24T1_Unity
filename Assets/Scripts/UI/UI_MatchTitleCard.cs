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
    MinigameConfig config;

    public void Initilize(MinigameConfig newConfig)
    {
        config = newConfig;
        matchTitle.text = config.Name;
        matchDescription.text = config.Description;
        if (config.StartTitleCardPicture != null) matchSplashImage.texture = config.StartTitleCardPicture;

        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToShow)
        {
            //Game.Match.Mode = MinigameMatch.EState.inProgress;

            config.Play();
            //gameObject.SetActive(false);
        }
    }
}
