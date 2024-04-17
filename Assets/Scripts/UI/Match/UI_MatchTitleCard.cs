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
    [SerializeField] float timeToShowWhenSkipped = 0.1f;
    
    float timer;
    MinigameConfig config;
    List<string> seenConfigs = new List<string>();

    private void OnDisable()
    {
        Game.InputReader.OnTouchPressed -= OnTouchScreen;
    }

    public void Initilize(MinigameConfig newConfig)
    {
        config = newConfig;
        matchTitle.text = config.Name;
        matchDescription.text = config.Description;
        if (config.StartTitleCardPicture != null) matchSplashImage.texture = config.StartTitleCardPicture;

        timer = 0;
        Game.InputReader.OnTouchPressed += OnTouchScreen;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= timeToShow)
        {
            //Game.Match.Mode = MinigameMatch.EState.inProgress;
            Game.Player.Level.SeenTitleCard(config);
            config.Play();
            //gameObject.SetActive(false);
        }
    }

    void OnTouchScreen()
    {
        
        if (!Game.Player.Level.HasSeenTitleCard(config)) return;
        if (timer >= timeToShow - timeToShowWhenSkipped) return;
        timer = timeToShow - timeToShowWhenSkipped;
        Debug.Log("Skip pressed");
    }
}
