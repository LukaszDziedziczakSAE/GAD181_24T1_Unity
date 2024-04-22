using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenuStatus : MonoBehaviour
{
    [SerializeField] Button settingsButton;
    [SerializeField] Button newMatchButton;
    [SerializeField] Button characterButton;
    [SerializeField] Button surveyButton;
    [SerializeField] Button creditsButton;
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playerLevel;
    [SerializeField] TMP_Text playerXp;
    [SerializeField] TMP_Text playerCurrencyHeld;
    [SerializeField] RawImage characterIcon;
    [SerializeField] UIMover mover;
    [SerializeField] UIMover button1Mover;
    [SerializeField] UIMover button2Mover;
    [SerializeField] string surveyURL;

    UI_MainMenu MainMenu => (UI_MainMenu)Game.UI;
    private void OnEnable()
    {
        if (mover != null)
        {
            mover.SetOffScreenPosition();
            mover.MoveToOnScreen();
            mover.MoveOnScreenComplete += ShowTutorial;
        }
        else ShowTutorial();
        if (button1Mover != null)
        {
            button1Mover.SetOffScreenPosition();
            button1Mover.MoveToOnScreen();
        }
        if (button2Mover != null)
        {
            button2Mover.SetOffScreenPosition();
            button2Mover.MoveToOnScreen();
        }

        settingsButton.onClick.AddListener(OnSettingButtonPress);
        newMatchButton.onClick.AddListener(OnNewMatchButtonPress);
        characterButton.onClick.AddListener(OnCharacterButtonPress);
        surveyButton.onClick.AddListener(OnSurveyButtonPress);
        creditsButton.onClick.AddListener(OnCreditsButtonPress);
        UpdateStatusBar();
    }

    private void OnDisable()
    {
        settingsButton.onClick.RemoveListener(OnSettingButtonPress);
        newMatchButton.onClick.RemoveListener(OnNewMatchButtonPress);
        characterButton.onClick.RemoveListener(OnCharacterButtonPress);
        surveyButton.onClick.RemoveListener(OnSurveyButtonPress);
        creditsButton.onClick.RemoveListener(OnCreditsButtonPress);
    }

    private void OnSettingButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        MainMenu.SettingsMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnNewMatchButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        MainMenu.SceneList.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnCharacterButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        MainMenu.CharacterList.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void UpdateStatusBar()
    {
        playerName.text = Game.Player.PlayerName;
        playerLevel.text = Game.Player.Level.Level.ToString();
        playerXp.text = Game.Player.Level.Experiance.ToString() + " / " + Game.Player.Level.CurrentRequriment.ToString();
        playerCurrencyHeld.text = Game.Player.Currency.AmountHeld.ToString();
        characterIcon.texture = Game.Player.CharacterConfig.Icon;
    }

    private void OnCreditsButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        MainMenu.Credits.gameObject.SetActive(!MainMenu.Credits.gameObject.activeSelf);
    }

    private void OnSurveyButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        Application.OpenURL(surveyURL);
    }

    private void ShowTutorial()
    {
        Game.Match.ShowTutorial(0);
    }
}
