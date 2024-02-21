using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class UI_PlayerProfile : MonoBehaviour
{
    [SerializeField] UI_MainMenu mainMenu;
    [SerializeField] Button closeButton;
    [SerializeField] TMP_InputField nameField;
    [SerializeField] Button nameButton;
    [SerializeField] TMP_Text nameButtonText;
    [SerializeField] TMP_Dropdown characterVerient;
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

    [SerializeField] List<string> characterNames;

    private void OnEnable()
    {
        characterNames = GetCharacterNames;
        UpdateCharacterProfile();

        closeButton.onClick.AddListener(OnCloseButtonPress);
        //nameField.onValueChanged.AddListener(NameFieldValueChanged);
        //characterVerient.onValueChanged.AddListener(CharacterVerientValueChanged);
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
        //nameField.onValueChanged.RemoveListener(NameFieldValueChanged);
        //characterVerient.onValueChanged.RemoveListener(CharacterVerientValueChanged);
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
        if (nameField == null)
        {
            Debug.LogError(name + ": missing nameField referance");
            return;
        }

        if (Game.Player == null)
        {
            Debug.LogError(name + ": missing Game.Player referance");
            return;
        }

        //nameField.text = Game.Player.PlayerName;
        nameButtonText.text = Game.Player.PlayerName;
        //if (characterVerient.options.Count <= 1) BuildDropDownList();
        if (Game.Player != null) characterVerient.value = (int)Game.Player.CharacterConfig.Variant;
        if (Game.PlayerCharacter.Model.CharacterConfig.CharacterName == null || Game.PlayerCharacter.Model.CharacterConfig.CharacterName == "")
            varientButtonText.text = Game.Player.CharacterConfig.Variant.ToString();
        else varientButtonText.text = Game.PlayerCharacter.Model.CharacterConfig.CharacterName;
        UpdateSkinButtons();
        UpdateColorButtons();

        Game.PlayerCharacter.Model.SetNewConfig(Game.Player.CharacterConfig);
    }


    private void BuildDropDownList()
    {
        string selectedVariant = Game.Player.CharacterConfig.Variant.ToString();

        characterVerient.ClearOptions();
        characterNames = GetCharacterNames;
        //Type enumType = CharacterModel.EVariant.Random.GetType();
        List< TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        int current = 0;
        for (int i = 0; i < /*Enum.GetNames(enumType).Length*/ characterNames.Count; i++)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(characterNames[i]);
            options.Add(optionData);

            if (optionData.text == selectedVariant) current = i;
        }
        characterVerient.AddOptions(options);
        if (current == 0) Debug.LogWarning(name + ": could not find " + Game.Player.CharacterConfig.Variant.ToString());

        
    }

    private void UpdateSkinButtons()
    {
        skin0Button.interactable = true;
        skin1Button.interactable = true;
        skin2Button.interactable = true;

        switch (Game.Player.CharacterConfig.Skin)
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

        switch(Game.Player.CharacterConfig.Color)
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

    private void CharacterVerientValueChanged(int newIndex)
    {
        string verientName = characterVerient.options[newIndex].text;
        CharacterModel.EVariant variant = CharacterModel.VariantName(verientName);
        //print("verientName=" + verientName + ", enumVal=" + ((CharacterModel.EVariant)newIndex).ToString() + ", characterName=" + CharacterNames[newIndex]);
        Game.Player.SetCharacterVarient(variant);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void NameFieldValueChanged(string newString)
    {
        Game.Player.ChangePlayerName(newString);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }
    private void OnSkin0ButtonPress()
    {
        Game.Player.SetCharacterSkin(0);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnSkin1ButtonPress()
    {
        Game.Player.SetCharacterSkin(1);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }
    private void OnSkin2ButtonPress()
    {
        Game.Player.SetCharacterSkin(2);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnYellowColorButtonPress()
    {
        Game.Player.SetCharacterColor(CharacterModel.EColor.Yello);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnRedColorButtonPress()
    {
        Game.Player.SetCharacterColor(CharacterModel.EColor.Red);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnPurpleColorButtonPress()
    {
        Game.Player.SetCharacterColor(CharacterModel.EColor.Purple);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnGreenColorButtonPress()
    {
        Game.Player.SetCharacterColor(CharacterModel.EColor.Green);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnBlueColorButtonPress()
    {
        Game.Player.SetCharacterColor(CharacterModel.EColor.Blue);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnDefaultColorButtonPress()
    {
        Game.Player.SetCharacterColor(CharacterModel.EColor.Default);
        Game.SaveSystem.SaveGameFile();
        UpdateCharacterProfile();
    }

    private void OnCloseButtonPress()
    {
        Game.CameraManager.SwitchTo(((MainMenuMatch)Game.Match).MainMenuCamera, ((MainMenuMatch)Game.Match).CameraBlendTime);
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
        mainMenu.PlayerProfile.gameObject.SetActive(false);
        mainMenu.CharacterList.gameObject.SetActive(true);
    }


    private void OnNameButtonPress()
    {
        mainMenu.EnterNameDialog.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
