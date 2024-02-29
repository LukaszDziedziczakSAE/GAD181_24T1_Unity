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

    List<UI_MatchEnd_CharacterListItem> characterListItems = new List<UI_MatchEnd_CharacterListItem>();

    private void OnEnable()
    {
        backgroundButton.onClick.AddListener(OnBackgroundButtonPress);
        playAgainButton.onClick.AddListener(OnPlayAgainButtonPress);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonPress);

        Initilise();
    }

    private void OnDisable()
    {
        backgroundButton.onClick.RemoveListener(OnBackgroundButtonPress);
        playAgainButton.onClick.RemoveListener(OnPlayAgainButtonPress);
        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonPress);
    }

    private void Initilise()
    {
        // scoreboard
        foreach (KeyValuePair<int, int> score in Game.Match.Result.Scores)
        {
            UI_MatchEnd_CharacterListItem characterListItem = Instantiate(matchEndListPrefab, matchEndList);
            characterListItem.Initilise(score.Key, score.Value);
            characterListItems.Add(characterListItem);
        }

        // xp gain

        // gold gain
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
}
