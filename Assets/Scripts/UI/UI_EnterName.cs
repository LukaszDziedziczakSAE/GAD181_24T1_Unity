using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnterName : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button enterButton;
    public bool NewPlayer;

    private void OnEnable()
    {
        enterButton.onClick.AddListener(OnEnterButtonPress);
        inputField.onValueChanged.AddListener(OnInputFieldChange);
        UpdateInputField();
    }

    private void OnDisable()
    {
        
    }

    private void OnEnterButtonPress()
    {
        
        if (!NewPlayer) ((UI_MainMenu)Game.UI).PlayerProfile.gameObject.SetActive(true);
        else
        {
            ((UI_MainMenu)Game.UI).MainMenuButtons.gameObject.SetActive(true);
            Game.Player.NewPlayer(inputField.text);
            Game.UpdatePlayersCharacterModel();
        }
        Game.SaveSystem.SaveGameFile();
        gameObject.SetActive(false);
    }

    private void UpdateInputField()
    {
         inputField.text = Game.Player.PlayerName;
    }

    private void OnInputFieldChange(string newInput)
    {
        if (NewPlayer) return;

        Game.Player.ChangePlayerName(newInput);
        UpdateInputField();
    }
}
