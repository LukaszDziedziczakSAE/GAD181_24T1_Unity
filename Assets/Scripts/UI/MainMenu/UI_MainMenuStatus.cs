using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenuStatus : MonoBehaviour
{
    [SerializeField] Button settingsButton;
    [SerializeField] Button newMatchButton;
    [SerializeField] Button characterButton;
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playerLevel;
    [SerializeField] TMP_Text playerXp;
    [SerializeField] TMP_Text playerCurrencyHeld;
    [SerializeField] RawImage characterIcon;
    UI_MainMenu MainMenu => (UI_MainMenu)Game.UI;
    private void OnEnable()
    {
        settingsButton.onClick.AddListener(OnSettingButtonPress);
        newMatchButton.onClick.AddListener(OnNewMatchButtonPress);
        characterButton.onClick.AddListener(OnCharacterButtonPress);
        UpdateStatusBar();
    }

    private void OnDisable()
    {
        settingsButton.onClick.RemoveListener(OnSettingButtonPress);
        newMatchButton.onClick.RemoveListener(OnNewMatchButtonPress);
        characterButton.onClick.RemoveListener(OnCharacterButtonPress);
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
}