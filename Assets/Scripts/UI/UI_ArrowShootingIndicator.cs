using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ArrowShootingIndicator : MonoBehaviour
{
    [SerializeField] private TMP_Text drawDistance;



    public void UpdateDrawDistance(float distance)
    {
        drawDistance.text = distance.ToString("F0");
    }
}
