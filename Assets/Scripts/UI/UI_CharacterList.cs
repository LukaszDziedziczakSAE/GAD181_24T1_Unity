using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class UI_CharacterList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] UI_CharacterListRow rowPrefab;
    List<UI_CharacterListRow> rows = new List<UI_CharacterListRow>();

    private void OnEnable()
    {
        Initilise();
    }

    private void Initilise()
    {
        foreach (UI_CharacterListRow row in  rows)
        {
            Destroy(row.gameObject);
        }
        rows.Clear();

        for (int i = 0; i <= Game.HighestLevel; i++)
        {
            if (Game.ConfigsUnlockedAt(i).Length == 0) continue;

            UI_CharacterListRow row = Instantiate(rowPrefab, content);
            row.Initilise(i);
            rows.Add(row);
        }
    }


}
