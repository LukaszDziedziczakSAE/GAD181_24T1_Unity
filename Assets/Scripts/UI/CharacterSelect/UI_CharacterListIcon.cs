using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterListIcon : MonoBehaviour
{
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text characterPrice;
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

        if (config.UnlockPrice > 0)
        {
            characterPrice.gameObject.SetActive(true);
            characterPrice.text = "$" + config.UnlockPrice;
        }
        else
        {
            characterPrice.gameObject.SetActive(false);
        }

        if (config.Icon == null)
        {
            config.SetIcon(Game.PlayerCharacter.Model.FindTextureByName(config.name));
        }

        if (config.Icon != null)
        {
            characterImage.texture = config.Icon;
        }
        else
        {
            Color color = Color.black;
            color.a = 0.5f;
            characterImage.color = color;
        }
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
