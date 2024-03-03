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

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToShow)
        {
            Game.Match.Mode = MinigameMatch.EState.inProgress;

            gameObject.SetActive(false);
        }
    }
}
