using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class UI_PlayerProfile : MonoBehaviour
{
    [SerializeField, Header("Referances")] UI_MainMenu mainMenu;
    [SerializeField] Button closeButton;
    [SerializeField] Button nameButton;
    [SerializeField] TMP_Text nameButtonText;
    [SerializeField] TMP_Text levelIndicatorText;
    [SerializeField] TMP_Text goldIndicatorText;
    [SerializeField] Button varientButton;
    [SerializeField] TMP_Text varientButtonText;
    [SerializeField] Button skin0Button;
    [SerializeField] Button skin1Button;
    [SerializeField] Button skin2Button;
    [SerializeField] Button defaultColorButton;
    [SerializeField] Button blueColorButton;
    [SerializeField] Button greenColorButton;
    [SerializeField] Button purpleColorButton;
    [SerializeField] Button redColorButton;
    [SerializeField] Button yellowColorButton;

    [SerializeField, Header("DEBUG")] List<string> characterNames;

    private void OnEnable()
    {
        characterNames = GetCharacterNames;
        UpdateCharacterProfile();

        closeButton.onClick.AddListener(OnCloseButtonPress);
        skin0Button.onClick.AddListener(OnSkin0ButtonPress);
        skin1Button.onClick.AddListener(OnSkin1ButtonPress);
        skin2Button.onClick.AddListener(OnSkin2ButtonPress);
        defaultColorButton.onClick.AddListener(OnDefaultColorButtonPress);
        blueColorButton.onClick.AddListener(OnBlueColorButtonPress);
        greenColorButton.onClick.AddListener(OnGreenColorButtonPress);
        purpleColorButton.onClick.AddListener(OnPurpleColorButtonPress);
        redColorButton.onClick.AddListener(OnRedColorButtonPress);
        yellowColorButton.onClick.AddListener(OnYellowColorButtonPress);
        varientButton.onClick.AddListener(OnVarientButtonPress);
        nameButton.onClick.AddListener(OnNameButtonPress);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(OnCloseButtonPress);
        skin0Button.onClick.RemoveListener(OnSkin0ButtonPress);
        skin1Button.onClick.RemoveListener(OnSkin1ButtonPress);
        skin2Button.onClick.RemoveListener(OnSkin2ButtonPress);
        defaultColorButton.onClick.RemoveListener(OnDefaultColorButtonPress);
        blueColorButton.onClick.RemoveListener(OnBlueColorButtonPress);
        greenColorButton.onClick.RemoveListener(OnGreenColorButtonPress);
        purpleColorButton.onClick.RemoveListener(OnPurpleColorButtonPress);
        redColorButton.onClick.RemoveListener(OnRedColorButtonPress);
        yellowColorButton.onClick.RemoveListener(OnYellowColorButtonPress);
        varientButton.onClick.RemoveListener(OnVarientButtonPress);
        nameButton.onClick.RemoveListener(OnNameButtonPress);
    }

    public void UpdateCharacterProfile()
    {
        /*if (nameField == null)
        {
            Debug.LogError(name + ": missing nameField referance");
            return;
        }*/

        if (Game.Player == null)
        {
            Debug.LogError(name + ": missing Game.Player referance");
            return;
        }

        nameButtonText.text = Game.Player.PlayerName;
        levelIndicatorText.text = Game.Player.Level.Level.ToString() /*+ " (" + Game.Player.Level.Experiance.ToString() + "/" + Game.Player.Level.CurrentRequriment.ToString() + ")"*/;
        goldIndicatorText.text = Game.Player.Currency.AmountHeld.ToString();

        UpdateCharacterName();

        UpdateSkinButtons();
        UpdateColorButtons();

        Game.PlayerCharacter.Model.SetNewConfig(Game.Player.CharacterModelConfig);
    }

    private void UpdateCharacterName()
    {

        if (Game.Player.CharacterConfig == null)
        {
            Debug.LogError("Player CharacterConfig not found");
            return;
        }

        if (Game.Player.CharacterConfig.CharacterName != null && Game.Player.CharacterConfig.CharacterName != "")
            varientButtonText.text = Game.Player.CharacterConfig.CharacterName;

        else
        {
            if (Game.Player.CharacterConfig.CharacterName == null) Debug.LogError(Game.Player.CharacterConfig.name + " has null Character Name");
            else if (Game.Player.CharacterConfig.CharacterName == "") Debug.LogError(Game.Player.CharacterConfig.name + " Character Name is empty");
            varientButtonText.text = Game.Player.CharacterModelConfig.Variant.ToString();
        }
    }

    private void UpdateSkinButtons()
    {
        skin0Button.interactable = true;
        skin1Button.interactable = true;
        skin2Button.interactable = true;

        switch (Game.Player.CharacterModelConfig.Skin)
        {
            case 0:
                skin0Button.interactable = false;
                break;

            case 1:
                skin1Button.interactable = false;
                break;

            case 2:
                skin2Button.interactable = false;
                break;
        }
    }

    private void UpdateColorButtons()
    {
        defaultColorButton.interactable = true;
        blueColorButton.interactable = true;
        greenColorButton.interactable = true;
        purpleColorButton.interactable = true;
        redColorButton.interactable = true; 
        yellowColorButton.interactable = true;

        switch(Game.Player.CharacterModelConfig.Color)
        {
            case CharacterModel.EColor.Default:
                defaultColorButton.interactable = false;
                break;

            case CharacterModel.EColor.Blue:
                blueColorButton.interactable = false;
                break;

            case CharacterModel.EColor.Green:
                greenColorButton.interactable = false;
                break;

            case CharacterModel.EColor.Purple:
                purpleColorButton.interactable = false;
                break;

            case CharacterModel.EColor.Red:
                redColorButton.interactable = false;
                break;

            case CharacterModel.EColor.Yello:
                yellowColorButton.interactable = false;
                break;
        }
    }

    private void OnSkin0ButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        Game.Player.SetCharacterSkin(0);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnSkin1ButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        Game.Player.SetCharacterSkin(1);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }
    private void OnSkin2ButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        Game.Player.SetCharacterSkin(2);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnYellowColorButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        Game.Player.SetCharacterColor(CharacterModel.EColor.Yello);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnRedColorButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        Game.Player.SetCharacterColor(CharacterModel.EColor.Red);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnPurpleColorButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        Game.Player.SetCharacterColor(CharacterModel.EColor.Purple);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnGreenColorButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        Game.Player.SetCharacterColor(CharacterModel.EColor.Green);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnBlueColorButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        Game.Player.SetCharacterColor(CharacterModel.EColor.Blue);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnDefaultColorButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        Game.Player.SetCharacterColor(CharacterModel.EColor.Default);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnCloseButtonPress()
    {
        Game.Sound.PlayButtonPressConfirmSound();
        Game.CameraManager.SwitchTo(((MainMenuMatch)Game.Match).MainMenuCameras[0], ((MainMenuMatch)Game.Match).CameraBlendTime);
        Game.CameraManager.BlendComplete += OnBackToMainMenuBlendComplete;
        gameObject.SetActive(false);
        //Game.SaveSystem.SaveGameFile();
    }

    private void OnBackToMainMenuBlendComplete()
    {
        mainMenu.MainMenuButtons.gameObject.SetActive(true);
    }

    private List<string> GetCharacterNames
    {
        get
        {
            List<string> list = new List<string>();

            Type enumType = CharacterModel.EVariant.Random.GetType();
            foreach(String name in Enum.GetNames(enumType))
            {
                list.Add(name);
            }

            return list;
        }
    }

    private void OnVarientButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        mainMenu.PlayerProfile.gameObject.SetActive(false);
        mainMenu.CharacterList.gameObject.SetActive(true);
    }


    private void OnNameButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        mainMenu.EnterNameDialog.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
