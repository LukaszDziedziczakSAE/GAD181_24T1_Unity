using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PauseMenu : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button exitMatchButton;

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(OnResumeButtonPress);
        settingsButton.onClick.AddListener(OnSettingsButtonPress);
        exitMatchButton.onClick.AddListener(OnExitMatchButtonPress);

        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnResumeButtonPress);
        settingsButton.onClick.RemoveListener(OnSettingsButtonPress);
        exitMatchButton.onClick.RemoveListener(OnExitMatchButtonPress);

        
    }

    private void OnResumeButtonPress()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnSettingsButtonPress()
    {
        Game.UI.SettingsMenu.gameObject.SetActive(true);
        Game.UI.SettingsMenu.backToPauseMenu = true;
        gameObject.SetActive(false);
    }

    private void OnExitMatchButtonPress()
    {
        Time.timeScale = 1;
        Game.InputReader.ClearEvents();
        Game.LoadMainMenu();
    }
}
