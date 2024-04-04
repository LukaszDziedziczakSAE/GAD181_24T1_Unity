using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Prematch : MonoBehaviour
{
    [SerializeField] TMP_Text countdown;

    private void Start()
    {
        Game.Sound.PlayPrematchCountdownSound();
    }

    private void Update()
    {
        if (countdown != null)
        {
            if (Game.Match.MatchTime >= -0.5f) countdown.text = "Start";
            else countdown.text = (Game.Match.MatchTime * -1).ToString("F0");
        }
    }
}
