using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArrowShootingIndicator : MonoBehaviour
{
    [SerializeField] private TMP_Text startingY;
    [SerializeField] private TMP_Text currentY;
    [SerializeField] private TMP_Text distance;
    [SerializeField] private TMP_Text power;
    [SerializeField] Color canFire = Color.green;
    [SerializeField] Color cannotFire = Color.red;
    [SerializeField] Image background;


    public void UpdateBackgroundColour(bool firable)
    {
        if(firable)
        {
            background.color = canFire;
        }
        else
        {
            background.color = cannotFire;
        }
    }
    public void UpdateDrawDistance(float starting, float current, float distance, float powerPercent)
    {
        startingY.text = "start " + starting.ToString("F2");
        currentY.text = "current " + current.ToString("F2");
        this.distance.text = "distance " + distance.ToString("F0");
        power.text = "power " + (powerPercent * 100).ToString("F0");
    }
}
