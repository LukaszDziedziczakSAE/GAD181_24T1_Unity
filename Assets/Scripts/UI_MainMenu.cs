using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] UI_SceneSelectButton sceneButtonPrefab;
    List<UI_SceneSelectButton> sceneSelectButtons = new List<UI_SceneSelectButton>();

    private void OnEnable()
    {
        UpdateList();
    }

    public void UpdateList()
    {
        ClearList();
        //print("SceneManager.sceneCountInBuildSettings=" + SceneManager.sceneCountInBuildSettings);
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            UI_SceneSelectButton sceneButton = Instantiate(sceneButtonPrefab, content);
            sceneSelectButtons.Add(sceneButton);
            sceneButton.Initilize(i);
        }
    }

    public void ClearList()
    {
        foreach(UI_SceneSelectButton button in sceneSelectButtons)
        {
            Destroy(button.gameObject);
        }
        sceneSelectButtons.Clear();
    }
}
