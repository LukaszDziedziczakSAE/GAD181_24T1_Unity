using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ScavangerHunt : UI_Main
{
    ScavangerHuntMatch match => (ScavangerHuntMatch)Game.Match;
    [SerializeField] TMP_Text touchPoint;
    [SerializeField] TMP_Text pickedUpAmount;
    [SerializeField] TMP_Text timeLeft;
    bool levelLoaded;

    private void OnDisable()
    {
        if (Game.Instance == null || Game.InputReader == null || !levelLoaded) return;
        //Game.InputReader.OnTouchPressed -= OnTouchPress;
        //Game.InputReader.OnTouchReleased -= OnTouchRelease;
    }

    private void Update()
    {
        if (!levelLoaded) return;

        if (touchPoint != null && touchPoint.gameObject.activeSelf) touchPoint.text = Game.InputReader.TouchPosition.ToString();
        //pickedUpAmount.text = match.itemsPickedUp.ToString();
        //timeLeft.text = match.MatchTimeRemaining.ToString("F0");
    }

    private void OnTouchPress()
    {
        touchPoint.gameObject.SetActive(true);
        touchPoint.text = Game.InputReader.TouchPosition.ToString();
    }

    private void OnTouchRelease()
    {
        touchPoint.gameObject.SetActive(false);
    }

    public override void LevelLoaded()
    {
       // Game.InputReader.OnTouchPressed += OnTouchPress;
        //Game.InputReader.OnTouchReleased += OnTouchRelease;
        touchPoint.gameObject.SetActive(false);
        //if (matchStart != null) matchStart.gameObject.SetActive(false);
        levelLoaded = true;
    }
}
