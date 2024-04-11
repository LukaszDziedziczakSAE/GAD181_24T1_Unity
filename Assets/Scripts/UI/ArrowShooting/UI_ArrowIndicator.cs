using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ArrowIndicator : MonoBehaviour
{
    [SerializeField] private TMP_Text power;

    public void UpdateArrowPower(TargetShooting_Arrow arrow)
    {
        power.text = (arrow.Power * 100).ToString("F0");
    }
}
