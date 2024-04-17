using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TutorialCard : MonoBehaviour
{
    //[SerializeField] MinigameConfig minigameConfig;
    [SerializeField] int tutorialIndex;
    [SerializeField] string tutorialTitle;
    [SerializeField] string tutorialText;

    [Header("Referances")]
    [SerializeField] TMP_Text tutotialTitleText;
    [SerializeField] TMP_Text tutorialTextbody;
    [SerializeField] Button closeButton;

    public int Index => tutorialIndex;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(OnCloseButtonPress);
        UpdateTutorialText();
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(OnCloseButtonPress);
    }

    private void OnCloseButtonPress()
    {
        Time.timeScale = 1;
        Game.Player.SeenTutorials.AddTutorialRecord(Game.Match.Config, tutorialIndex);
        gameObject.SetActive(false);
    }

    private void UpdateTutorialText()
    {
        tutotialTitleText.text = tutorialTitle;
        tutorialTextbody.text = tutorialText;
    }
}

