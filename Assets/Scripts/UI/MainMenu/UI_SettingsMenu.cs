using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_SettingsMenu : MonoBehaviour
{
    public bool backToPauseMenu;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] TMP_Text musicVolumeText;
    [SerializeField] Slider matchVolumeSlider;
    [SerializeField] TMP_Text matchVolumeText;
    [SerializeField] Slider uiVolumeSlider;
    [SerializeField] TMP_Text uiVolumeText;
    [SerializeField] Button playerResetButton;
    [SerializeField] Button backButton;
    [SerializeField] Button lowGraphicsButton;
    [SerializeField] Button mediumGraphicsButton;
    [SerializeField] Button highGraphicsButton;

    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackButtonPress);
        playerResetButton.onClick.AddListener(OnPlayerResetButtonPress);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChange);
        matchVolumeSlider.onValueChanged.AddListener (OnMatchVolumeSliderChange);
        uiVolumeSlider.onValueChanged.AddListener(OnUiVolumeSliderChange);
        lowGraphicsButton.onClick.AddListener(OnLowGraphicsButtonPress);
        mediumGraphicsButton.onClick.AddListener(OnMediumGraphicsButtonPress);
        highGraphicsButton.onClick.AddListener(OnHighGraphicsButtonPress);
        UpdateSettingsUI();
    }

    private void OnDisable()
    {
        backToPauseMenu = false;
        playerResetButton.gameObject.SetActive(true);
        backButton.onClick.RemoveListener(OnBackButtonPress);
        playerResetButton.onClick.RemoveListener(OnPlayerResetButtonPress);
        musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeSliderChange);
        matchVolumeSlider.onValueChanged.RemoveListener(OnMatchVolumeSliderChange);
        uiVolumeSlider.onValueChanged.RemoveListener(OnUiVolumeSliderChange);
        lowGraphicsButton.onClick.RemoveListener(OnLowGraphicsButtonPress);
        mediumGraphicsButton.onClick.RemoveListener(OnMediumGraphicsButtonPress);
        highGraphicsButton.onClick.RemoveListener(OnHighGraphicsButtonPress);
    }

    public void OpenedFromPauseMenu()
    {
        backToPauseMenu = true;
        playerResetButton.gameObject.SetActive(false);
    }

    private void UpdateSettingsUI()
    {
        if (audioMixer.GetFloat("MusicVolume", out float musicVolume))
        {
            musicVolumeSlider.value = musicVolume;
            musicVolumeText.text = (musicVolume + 80).ToString("F0") + "%";
        }
        else
        {
            musicVolumeSlider.gameObject.SetActive(false);
            musicVolumeText.gameObject.SetActive(false);
        }

        if (audioMixer.GetFloat("MatchVolume", out float matchVolume))
        {
            matchVolumeSlider.value = matchVolume;
            matchVolumeText.text = (matchVolume + 80).ToString("F0") + "%";
        }
        else
        {
            matchVolumeSlider.gameObject.SetActive(false);
            matchVolumeText.gameObject.SetActive(false);
        }

        if (audioMixer.GetFloat("UIVolume", out float uiVolume))
        {
            uiVolumeSlider.value = uiVolume;
            uiVolumeText.text = (uiVolume + 80).ToString("F0") + "%";
        }
        else
        {
            uiVolumeSlider.gameObject.SetActive(false);
            uiVolumeText.gameObject.SetActive(false);
        }

        switch (Game.Player.Settings.GraphicQualityLevel)
        {
            case 0:
                lowGraphicsButton.interactable = false;
                mediumGraphicsButton.interactable = true;
                highGraphicsButton.interactable = true;
                break;

            case 1:
                lowGraphicsButton.interactable = true;
                mediumGraphicsButton.interactable = false;
                highGraphicsButton.interactable = true;
                break;

            case 2:
                lowGraphicsButton.interactable = true;
                mediumGraphicsButton.interactable = true;
                highGraphicsButton.interactable = false;
                break;

            default:
                lowGraphicsButton.interactable = true;
                mediumGraphicsButton.interactable = true;
                highGraphicsButton.interactable = true;
                break;
        }
    }

    private float VolumeNormalised(float volume)
    {
        return (volume + 80) / 100;
    }

    private void OnBackButtonPress()
    {
        Game.Sound.PlayButtonPressCancelSound();
        if (backToPauseMenu)
        {
            Game.UI.PauseMenu.gameObject.SetActive(true);
        }
        else
        {
            ((UI_MainMenu)Game.UI).MainMenuStatus.gameObject.SetActive(true);
        }
        Game.Save();
        gameObject.SetActive(false);

    }

    private void OnPlayerResetButtonPress()
    {
        Game.Sound.PlayButtonPressConfirmSound();
        Game.ResetPlayer();
    }

    private void OnMusicVolumeSliderChange(float newValue)
    {
        audioMixer.SetFloat("MusicVolume", newValue);
        UpdateSettingsUI();
    }

    private void OnMatchVolumeSliderChange(float newValue)
    {
        audioMixer.SetFloat("MatchVolume", newValue);
        UpdateSettingsUI();
    }

    private void OnUiVolumeSliderChange(float newValue)
    {
        audioMixer.SetFloat("UIVolume", newValue);
        UpdateSettingsUI();
    }

    private void OnLowGraphicsButtonPress()
    {
        Game.Player.Settings.SetQualityLevel(0);
        UpdateSettingsUI();
    }

    private void OnMediumGraphicsButtonPress()
    {
        Game.Player.Settings.SetQualityLevel(1);
        UpdateSettingsUI();
    }

    private void OnHighGraphicsButtonPress()
    {
        Game.Player.Settings.SetQualityLevel(2);
        UpdateSettingsUI();
    }
}
