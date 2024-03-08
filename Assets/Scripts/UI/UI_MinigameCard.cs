using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MinigameCard : MonoBehaviour
{
    [SerializeField] TMP_Text gameTitle;
    [SerializeField] RawImage gameCardImage;
    [SerializeField] Button button;

    MinigameConfig config;
    UI_SceneList sceneList;

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonPress);
        
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonPress);
    }

    public void Initilize(MinigameConfig newConfig, UI_SceneList ui)
    {
        sceneList = ui;
        config = newConfig;
        gameTitle.text = config.Name;
        if (config.SelectionCardPicture != null) gameCardImage.texture = config.SelectionCardPicture;
    }

    private void OnButtonPress()
    {
        Game.Sound.PlayButtonPressConfirmSound();
        //miniGame.Play();
        sceneList.PlayMiniGame(config);
    }
}
