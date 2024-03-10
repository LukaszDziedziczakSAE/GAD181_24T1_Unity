using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class UI_CharacterList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] UI_CharacterCard characterCardPrefab;
    [SerializeField] Button backButton;
    List<UI_CharacterCard> characterCards = new List<UI_CharacterCard>();

    private void OnEnable()
    {
        Initilise();
        backButton.onClick.AddListener(OnBackButtonPress);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveListener(OnBackButtonPress);
    }

    private void Initilise()
    {
        foreach (UI_CharacterCard card in characterCards)
        {
            Destroy(card.gameObject);
        }
        characterCards.Clear();

        for (int i = 0; i <= Game.HighestLevel; i++)
        {
            if (Game.ConfigsUnlockedAt(i).Length == 0) continue;

            foreach (CharacterConfig config in Game.ConfigsUnlockedAt(i))
            {
                UI_CharacterCard card = Instantiate(characterCardPrefab, content);
                card.Initilise(config, this);
                characterCards.Add(card);
            }
        }
    }

    private void OnBackButtonPress()
    {
        Game.Sound.PlayButtonPressCancelSound();
        UI_MainMenu mainMenu = (UI_MainMenu)Game.UI;

        mainMenu.CharacterList.gameObject.SetActive(false);
        mainMenu.PlayerProfile.gameObject.SetActive(true);
    }

    public void ReinitilizeCards()
    {
        foreach (UI_CharacterCard card in characterCards)
        {
            card.Reinitilize();
        }
    }
}
