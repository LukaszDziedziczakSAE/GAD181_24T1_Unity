using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CharacterListRow : MonoBehaviour
{
    [SerializeField] Transform iconBox;
    [SerializeField] TMP_Text level;
    [SerializeField] UI_CharacterListIcon iconPrefab;
    List<UI_CharacterListIcon> icons = new List<UI_CharacterListIcon>();

    public void Initilise(int level)
    {
        if ( level > 0) this.level.text = level.ToString();
        else this.level.gameObject.SetActive(false);

        foreach(CharacterConfig config in Game.ConfigsUnlockedAt(level))
        {
            UI_CharacterListIcon icon = Instantiate(iconPrefab, iconBox);
            icon.Initilise(config);
            icons.Add(icon);
        }
    }
}
