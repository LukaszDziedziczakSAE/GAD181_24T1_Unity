using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterModel;

public class Player : MonoBehaviour
{
    [field: SerializeField] public string PlayerName {  get; private set; }
    [field: SerializeField] public  CharacterModel.Config CharacterConfig { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


    public void SetCharacterVarient(CharacterModel.EVariant newVarient)
    {
        CharacterConfig = new CharacterModel.Config(newVarient, CharacterConfig.Skin, CharacterConfig.Color);
    }

    public void SetCharacterSkin(int skinIndex)
    {
        CharacterConfig = new CharacterModel.Config(CharacterConfig.Variant, skinIndex, CharacterConfig.Color);
    }

    public void SetCharacterColor(CharacterModel.EColor newColor)
    {
        CharacterConfig = new CharacterModel.Config(CharacterConfig.Variant, CharacterConfig.Skin, newColor);
    }

    public void ChangePlayerName(string newName)
    {
        PlayerName = newName;
    }

}
