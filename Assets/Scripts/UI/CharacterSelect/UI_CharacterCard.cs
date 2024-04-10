using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterCard : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image lockForeground; 
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text characterPrice;
    [SerializeField] TMP_Text characterDescription;
    [SerializeField] TMP_Text characterLevel;
    [SerializeField] GameObject levelBox;
    [SerializeField] RawImage characterImage;
    [SerializeField] Button button;

    UI_CharacterList characterList;
    CharacterConfig config;
    EMode mode;

    public EMode Mode
    {
        get { return mode; }
        set { SetMode(value); }
    }

    public enum EMode
    {
        None,
        Selectable,
        Purchasable,
        CannotAfford,
        Locked
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonPress);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonPress);
    }

    public void Initilise(CharacterConfig characterConfig, UI_CharacterList ui)
    {
        characterList = ui;
        name = characterConfig.Variant.ToString()+"(Card)";
        config = characterConfig;
        Reinitilize();
    }

    private void UpdateIcon()
    {
        if (config.Icon == null)
        {
            config.SetIcon(Game.PlayerCharacter.Model.FindTextureByName(config.name));
        }

        if (config.Icon != null) characterImage.texture = config.Icon;
    }

    private void UpdateUnlockLevel()
    {
        if (config.UnlockLevel > 0)
        {
            levelBox.SetActive(true);
            characterLevel.text = config.UnlockLevel.ToString();
        }
        else levelBox.SetActive(false);
    }

    private void UpdatePrice()
    {
        if (characterPrice == null) return;

        if (config.UnlockPrice > 0 && Mode != EMode.Selectable)
        {
            characterPrice.gameObject.SetActive(true);
            characterPrice.text = "$" + config.UnlockPrice.ToString();
        }
            
        else characterPrice.gameObject.SetActive(false);
    }

    private void UpdateDescription()
    {
        if (characterDescription == null) return;

        if (Mode == EMode.Selectable)
        {
            characterDescription.gameObject.SetActive(true);
            characterDescription.text = config.CharacterDescription;
        }
        else
        {
            characterDescription.gameObject.SetActive(false);
        }
            
    }

    private void UpdateName()
    {
        if (config.CharacterName != null && config.CharacterName != "")
        {
            characterName.text = config.CharacterName;
        }
        else characterName.text = config.nameFixed;
    }

    private void OnButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        UI_MainMenu mainMenu = (UI_MainMenu)Game.UI;

        switch(Mode)
        {
            case EMode.Selectable:
                Game.Player.SetCharacterVarient(config.Variant);
                Game.SaveSystem.SaveGameFile();
                /*mainMenu.CharacterList.gameObject.SetActive(false);
                //mainMenu.PlayerProfile.gameObject.SetActive(true);
                mainMenu.MainMenuStatus.gameObject.SetActive(true);*/
                characterList.OnBackButtonPress();
                break;

            case EMode.Purchasable:
                Game.Player.Currency.UnlockCharacter(config);
                //Reinitilize();
                characterList.ReinitilizeCards();
                Game.SaveSystem.SaveGameFile();
                break;
        }
        
    }

    private void SetMode(EMode newMode)
    {
        mode = newMode;
        UpdateBackground();
        UpdateName();
        UpdateDescription();
        UpdatePrice();
        UpdateUnlockLevel();
        UpdateIcon();
    }

    private void UpdateBackground()
    {
        switch(Mode)
        {
            case EMode.None: 
                background.color = Color.clear;
                lockForeground.gameObject.SetActive(false);
                break;

            case EMode.Selectable:
                background.color = Color.white;
                lockForeground.gameObject.SetActive(false);
                break;

            case EMode.Purchasable:
                background.color = Color.green;
                lockForeground.gameObject.SetActive(false);
                break;

            case EMode.CannotAfford:
                background.color = Color.red;
                lockForeground.gameObject.SetActive(true);
                break;

            case EMode.Locked:
                background.color = Color.gray;
                lockForeground.gameObject.SetActive(true);
                break;
        }
    }

    public void Reinitilize()
    {
        if (config.UnlockLevel == 0 ||
            Game.Player.Currency.CharacterIsUnlocked(config.Variant))
            Mode = EMode.Selectable;

        else if (Game.Player.Level.Level >= config.UnlockLevel)
        {
            if (Game.Player.Currency.CanAffordCharacter(config))
                Mode = EMode.Purchasable;

            else Mode = EMode.CannotAfford;
        }

        else
        {
            //Debug.LogWarning("Level = " + Game.Player.Level.Level.ToString() + ", ConfigUnlockLevel = " + config.UnlockLevel);
            Mode = EMode.Locked;
        }
        

        Debug.Log(name + ": set mode " + Mode.ToString());
    }
}
