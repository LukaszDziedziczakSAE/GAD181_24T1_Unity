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

    Minigames.Game miniGame;
    UI_SceneList sceneList;

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonPress);
        
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonPress);
    }

    public void Initilize(Minigames.Game game, UI_SceneList ui)
    {
        sceneList = ui;
        miniGame = game;
        gameTitle.text = miniGame.Name;
        if (miniGame.selectionCardPicture != null) gameCardImage.texture = miniGame.selectionCardPicture;
    }

    private void OnButtonPress()
    {
        Game.Sound.PlayButtonPressConfirmSound();
        //miniGame.Play();
        sceneList.PlayMiniGame(miniGame);
    }
}
