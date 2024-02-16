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

    private void OnEnable()
    {
        CloseAll();
        MainMenuButtons.gameObject.SetActive(true);
    }

    public void CloseAll()
    {
        SceneList.gameObject.SetActive(false);
        MainMenuButtons.gameObject.SetActive(false);
        PlayerProfile.gameObject.SetActive(false);
    }
}
