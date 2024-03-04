using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterModel;

public class Player : MonoBehaviour, ISaveable
{
    [field: SerializeField] public string PlayerName { get; private set; } = "";
    [field: SerializeField] public  CharacterModel.Config CharacterConfig { get; private set; }
    [field: SerializeField] public PlayerLevel Level { get; private set; }
    [field: SerializeField] public PlayerCurrency Currency { get; private set; }

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

    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();

        state.Add("PlayerName", PlayerName);
        state.Add("CharacterConfig", CharacterConfig);

        return state;
    }

    public void RestoreState(object state)
    {
        Dictionary<string, object> restoredState = (Dictionary<string, object>)state;

        PlayerName = (string)restoredState["PlayerName"];
        CharacterConfig = (CharacterModel.Config)restoredState["CharacterConfig"];
    }

    public void NewPlayer(string name)
    {
        ChangePlayerName(name);
        SetCharacterVarient(CharacterModel.EVariant.Adventure_Peasant_01);
        SetCharacterColor(CharacterModel.EColor.Default);
        SetCharacterSkin(0);
    }

    public void ClearPlayer()
    {
        NewPlayer("");

    }
}
