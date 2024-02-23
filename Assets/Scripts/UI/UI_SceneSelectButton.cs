using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SceneSelectButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TMP_Text buttonText;
    [SerializeField] string[] sceneNames;
    int index;

    Scene scene =>
        SceneManager.GetSceneByBuildIndex(index);

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonPress);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonPress);
    }

    private void OnButtonPress()
    {
        SceneManager.LoadScene(index);
    }

    public void Initilize(int index)
    {
        this.index = index;
        buttonText.text = /*"(" + index + ") " + */sceneNames[this.index];
    }
}
