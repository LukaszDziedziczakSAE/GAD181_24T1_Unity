using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterCard : MonoBehaviour
{
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text characterPrice;
    [SerializeField] TMP_Text characterDescription;
    [SerializeField] TMP_Text characterLevel;
    [SerializeField] GameObject levelBox;
    [SerializeField] RawImage characterImage;
    [SerializeField] Button button;

    CharacterConfig config;

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonPress);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonPress);
    }

    public void Initilise(CharacterConfig characterConfig)
    {
        config = characterConfig;

        if (config.CharacterName != null && config.CharacterName != "")
        {
            characterName.text = config.CharacterName;
        }
        else characterName.text = config.nameFixed;

        if (characterDescription != null) characterDescription.text = config.CharacterDescription;

        if (config.UnlockPrice > 0)
            characterPrice.text = "$" + config.UnlockPrice.ToString();
        else characterPrice.gameObject.SetActive(false);

        if (config.UnlockLevel > 0) characterLevel.text = config.UnlockLevel.ToString();
        else levelBox.SetActive(false);

        if (config.Icon == null)
        {
            config.SetIcon(Game.PlayerCharacter.Model.FindTextureByName(config.name));
        }

        if (config.Icon != null) characterImage.texture = config.Icon;


    }

    private void OnButtonPress()
    {
        Game.Sound.PlayButtonPressSound();
        UI_MainMenu mainMenu = (UI_MainMenu)Game.UI;

        Game.Player.SetCharacterVarient(config.Variant);
        Game.SaveSystem.SaveGameFile();
        mainMenu.CharacterList.gameObject.SetActive(false);
        mainMenu.PlayerProfile.gameObject.SetActive(true);
    }
}
