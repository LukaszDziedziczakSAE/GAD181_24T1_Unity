using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SettingsMenu : MonoBehaviour
{
    public bool backToPauseMenu;
    [SerializeField] Button backButton;


    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackButtonPress);
    }

    private void OnDisable()
    {
        backToPauseMenu = false;
        backButton.onClick.RemoveListener(OnBackButtonPress);
    }

    private void OnBackButtonPress()
    {
        if (backToPauseMenu)
        {
            Game.UI.PauseMenu.gameObject.SetActive(true);
        }
        else
        {
            ((UI_MainMenu)Game.UI).MainMenuButtons.gameObject.SetActive(true);
        }

        gameObject.SetActive(false);

    }
}
