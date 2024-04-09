using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MatchEnd : MonoBehaviour
{
    [SerializeField] Button backgroundButton;
    [SerializeField] Button playAgainButton;
    [SerializeField] Button mainMenuButton;

    [SerializeField] UI_MatchEnd_CharacterListItem matchEndListPrefab;
    [SerializeField] Transform matchEndList;
    [SerializeField] TMP_Text playerCurrency;
    [SerializeField] TMP_Text playerCurrencyErned;
    [SerializeField] TMP_Text playerExperience;
    [SerializeField] TMP_Text playerExperienceErned;
    [SerializeField] TMP_Text playerLevel;
    [SerializeField] Image playerPreviousXPIndicator;
    [SerializeField] Image playerCurrentXPIndicator;
    [SerializeField] UIMover mover;

    List<UI_MatchEnd_CharacterListItem> characterListItems = new List<UI_MatchEnd_CharacterListItem>();
    MatchResult matchResult;
    MatchResult.Result playerResult;

    private void OnEnable()
    {
        if (mover != null)
        {
            mover.SetOffScreenPosition();
            mover.MoveToOnScreen();
        }

        backgroundButton.onClick.AddListener(OnBackgroundButtonPress);
        playAgainButton.onClick.AddListener(OnPlayAgainButtonPress);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonPress);
    }

    private void OnDisable()
    {
        backgroundButton.onClick.RemoveListener(OnBackgroundButtonPress);
        playAgainButton.onClick.RemoveListener(OnPlayAgainButtonPress);
        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonPress);
    }

    public void Initilise(MatchResult newResult)
    {
        matchResult = newResult;
        playerResult = FindLocalPlayerResults();
        if (playerResult == null) return;
        UpdateScoreboard();
        UpdateXPGain();
        UpdateGoldGain();
        Game.Player.MatchComplete();
        Game.SaveSystem.SaveGameFile();

        
        if (Game.Instance != null && Game.Music != null && playerResult != null)
        {
            bool playerWon = playerResult.Placement == 1;
            if (playerWon)
            {
                Game.Music.PlayVictoryTrack();
            }
            else
            {
                Game.Music.PlayDefeatTrack();
            }
        }
        
    }

    private void Update()
    {
        if (Game.Instance != null && Game.Music != null && 
            !Game.Music.IsPlaying) Game.Music.PlayPostMatchTrack();
    }

    private void UpdateGoldGain()
    {
        int playerPreviousCurrency = Game.Player.Currency.AmountHeld;

        Game.Player.Currency.AddCurrency(playerResult.GoldAward);

        playerCurrency.text = Game.Player.Currency.AmountHeld.ToString();
        playerCurrencyErned.text = "+" + playerResult.GoldAward.ToString();
    }

    private void UpdateXPGain()
    {
        int previousXP = Game.Player.Level.Experiance;

        Game.Player.Level.AddExperiance(playerResult.XPAward);

        playerExperience.text = Game.Player.Level.Experiance.ToString();
        playerExperienceErned.text = "+" + playerResult.XPAward.ToString();
        playerLevel.text = Game.Player.Level.Level.ToString();

        float previousXPNormalized = (float)previousXP / Game.Player.Level.CurrentRequriment;
        float currentXPNormalized = (float)Game.Player.Level.Experiance / Game.Player.Level.CurrentRequriment;

        playerPreviousXPIndicator.rectTransform.localScale = new Vector3(previousXPNormalized, 1, 1);
        playerCurrentXPIndicator.rectTransform.localScale = new Vector3(currentXPNormalized, 1,1);
    }

    private void UpdateScoreboard()
    {
        foreach (MatchResult.Result result in matchResult.Results)
        {
            UI_MatchEnd_CharacterListItem characterListItem = Instantiate(matchEndListPrefab, matchEndList);
            characterListItem.Initilise(result.PlayerNumber, result.Score);
            characterListItems.Add(characterListItem);
        }
    }

    private void OnBackgroundButtonPress()
    {
        Game.Sound.PlayButtonPressConfirmSound();
        Debug.Log("Background Button Pressed");
    }

    private void OnPlayAgainButtonPress()
    {
        Game.Sound.PlayButtonPressConfirmSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnMainMenuButtonPress()
    {
        Game.LoadMainMenu();
    }

    private MatchResult.Result FindLocalPlayerResults()
    {
        foreach (MatchResult.Result result in matchResult.Results)
        {
            if (result.PlayerNumber == 0) return result;

        }
        Debug.LogWarning("no local player result found");
        return null;
    }
}
