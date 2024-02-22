using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : UI_Main
{
    [field:SerializeField] public UI_SceneList SceneList {  get; private set; }
    [field: SerializeField] public UI_MainMenuButtons MainMenuButtons { get; private set; }
    [field: SerializeField] public UI_PlayerProfile PlayerProfile { get; private set; }
    [field: SerializeField] public UI_CharacterList CharacterList { get; private set; }
    [field: SerializeField] public UI_EnterName EnterNameDialog { get; private set; }

    private void OnEnable()
    {
        CloseAll();
        if (Game.Instance == null || Game.Player == null) return;
        //OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        CloseAll();
        if ((Game.Player.PlayerName == null || Game.Player.PlayerName == ""))
        {
            EnterNameDialog.gameObject.SetActive(true);
            EnterNameDialog.NewPlayer = true;
        }
        else MainMenuButtons.gameObject.SetActive(true);
        Game.UpdatePlayersCharacterModel();
    }

    public void CloseAll()
    {
        SceneList.gameObject.SetActive(false);
        MainMenuButtons.gameObject.SetActive(false);
        PlayerProfile.gameObject.SetActive(false);
    }

    public override void LevelLoaded()
    {
    }
}
