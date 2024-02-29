using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SceneList : MonoBehaviour
{
    [SerializeField] UI_MainMenu mainMenu;
    [SerializeField] Button backButton;
    [SerializeField] Transform content;
    [SerializeField] UI_SceneSelectButton sceneButtonPrefab;
    List<UI_SceneSelectButton> sceneSelectButtons = new List<UI_SceneSelectButton>();

    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackButtonPress);
        UpdateList();
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveListener(OnBackButtonPress);
    }

    public void UpdateList()
    {
        ClearList();
        //print("SceneManager.sceneCountInBuildSettings=" + SceneManager.sceneCountInBuildSettings);
        for (int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            UI_SceneSelectButton sceneButton = Instantiate(sceneButtonPrefab, content);
            sceneSelectButtons.Add(sceneButton);
            sceneButton.Initilize(i);
        }
    }

    public void ClearList()
    {
        foreach (UI_SceneSelectButton button in sceneSelectButtons)
        {
            Destroy(button.gameObject);
        }
        sceneSelectButtons.Clear();
    }

    public void OnBackButtonPress()
    {
        Game.Sound.PlayButtonPressCancelSound();
        mainMenu.MainMenuButtons.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
