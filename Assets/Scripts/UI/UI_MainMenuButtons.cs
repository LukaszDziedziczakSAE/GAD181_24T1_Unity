using QFSW.QC;
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
        debugButton.interactable = true;
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
        Game.CameraManager.SwitchTo(((MainMenuMatch)Game.Match).CharacterCamera, ((MainMenuMatch)Game.Match).CameraBlendTime);
        MainMenu.CloseAll();
        Game.CameraManager.BlendComplete += PlayerProfileCamBlendFinish;
    }

    private void OnSettingsButtonPress()
    {
        Game.UI.SettingsMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnDebugButtonPress()
    {
        //MainMenu.CloseAll();

        //MainMenu.CharacterList.gameObject.SetActive(true);
        //Game.LoadPortraitMaker();

        QuantumConsole.Instance.Activate();
    }

    private void PlayerProfileCamBlendFinish()
    {
        MainMenu.PlayerProfile.gameObject.SetActive(true);
    }
}
