using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Credits : MonoBehaviour
{
    [SerializeField] Button backButton;

    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackButtonPress);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveListener(OnBackButtonPress);
    }

    private void OnBackButtonPress()
    {
        Game.Sound.PlayButtonPressCancelSound();
        gameObject.SetActive(false);
    }
}
