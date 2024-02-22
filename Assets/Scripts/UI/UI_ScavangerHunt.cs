using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ScavangerHunt : UI_Main
{
    ScavangerHuntMatch match => (ScavangerHuntMatch)Game.Match;
    [SerializeField] TMP_Text touchPoint;
    [SerializeField] TMP_Text pickedUpAmount;

    private void OnDisable()
    {
        Game.InputReader.OnTouchPressed -= OnTouchPress;
        Game.InputReader.OnTouchReleased -= OnTouchRelease;
    }

    private void Update()
    {
        touchPoint.text = Game.InputReader.TouchPosition.ToString();
        pickedUpAmount.text = match.itemsPickedUp.ToString();
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
        Game.InputReader.OnTouchPressed += OnTouchPress;
        Game.InputReader.OnTouchReleased += OnTouchRelease;
        touchPoint.gameObject.SetActive(false);
    }
}
