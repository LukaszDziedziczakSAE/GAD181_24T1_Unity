using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerSlider : MonoBehaviour
{
    [SerializeField] Slider power;

  
    public void UpdateArrowPower(float arrowPower)
    {
        power.value = arrowPower;
    }
}
