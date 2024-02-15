using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenuButtons : MonoBehaviour
{
    [SerializeField] UI_MainMenu MainMenu;
    [SerializeField] Button startMatchButton;
    [SerializeField] Button playerProfileButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button debugButton;

    private void OnEnable()
    {
        startMatchButton.onClick.AddListener(OnStartMatchButtonPress);
        playerProfileButton.onClick.AddListener(OnPlayerProfileButtonPress);
        settingsButton.onClick.AddListener(OnSettingsButtonPress);
        debugButton.onClick.AddListener(OnDebugButtonPress);
    }

    private void OnDisable()
    {
        startMatchButton.onClick.RemoveListener(OnStartMatchButtonPress);
        playerProfileButton.onClick.RemoveListener(OnPlayerProfileButtonPress);
        settingsButton.onClick.RemoveListener(OnSettingsButtonPress);
        debugButton.onClick.RemoveListener(OnDebugButtonPress);
    }

    private void OnStartMatchButtonPress()
    {
        MainMenu.CloseAll();
        MainMenu.SceneList.gameObject.SetActive(true);
    }

    private void OnPlayerProfileButtonPress()
    {
        MainMenu.CloseAll();
        MainMenu.PlayerProfile.gameObject.SetActive(true);
    }

    private void OnSettingsButtonPress()
    {

    }

    private void OnDebugButtonPress()
    {
        MainMenu.CloseAll();
        MainMenu.CharacterList.gameObject.SetActive(true);

        //Game.LoadPortraitMaker();
    }
}
