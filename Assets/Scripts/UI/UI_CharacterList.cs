using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class UI_CharacterList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] UI_CharacterCard characterCardPrefab;
    List<UI_CharacterCard> characterCards = new List<UI_CharacterCard>();

    private void OnEnable()
    {
        Initilise();
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
                card.Initilise(config);
                characterCards.Add(card);
            }
        }
    }


}
