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
    [SerializeField] UI_MinigameCard minigameCardPrefab;
    List<UI_MinigameCard> minigameCards = new List<UI_MinigameCard>();

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

        foreach (MinigameConfig game in Game.Minigames.RandomGames)
        {
            UI_MinigameCard card = Instantiate(minigameCardPrefab, content);
            minigameCards.Add(card);
            card.Initilize(game, this);
        }
    }

    public void ClearList()
    {
        foreach (UI_MinigameCard card in minigameCards)
        {
            Destroy(card.gameObject);
        }
        minigameCards.Clear();
    }

    public void OnBackButtonPress()
    {
        Game.Sound.PlayButtonPressCancelSound();
        mainMenu.MainMenuStatus.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void PlayMiniGame(MinigameConfig config)
    {
        mainMenu.MatchStart.gameObject.SetActive(true);
        mainMenu.MatchStart.Initilize(config);
    }
}
