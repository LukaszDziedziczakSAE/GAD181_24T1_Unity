using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArrowShootingIndicator : MonoBehaviour
{
    [SerializeField] private TMP_Text drawDistance;
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
    public void UpdateDrawDistance(float distance)
    {
        drawDistance.text = distance.ToString("F0");
    }
}
