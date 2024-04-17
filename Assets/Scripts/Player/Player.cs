using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterModel;

public class Player : MonoBehaviour, ISaveable
{
    [field: SerializeField] public string PlayerName { get; private set; } = "";
    [field: SerializeField] public  CharacterModel.Config CharacterModelConfig { get; private set; }
    [field: SerializeField] public PlayerLevel Level { get; private set; }
    [field: SerializeField] public PlayerCurrency Currency { get; private set; }
    [field: SerializeField] public int MatchesPlayed { get; private set; }
    [field: SerializeField] public SeenTutorials SeenTutorials { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public CharacterConfig CharacterConfig
    {
        get
        {
            return Game.CharacterConfigByVarient(CharacterModelConfig.Variant);
        }
    }

    public void SetCharacterVarient(CharacterModel.EVariant newVarient)
    {
        CharacterModelConfig = new CharacterModel.Config(newVarient, CharacterModelConfig.Skin, CharacterModelConfig.Color);
    }

    public void SetCharacterSkin(int skinIndex)
    {
        CharacterModelConfig = new CharacterModel.Config(CharacterModelConfig.Variant, skinIndex, CharacterModelConfig.Color);
    }

    public void SetCharacterColor(CharacterModel.EColor newColor)
    {
        CharacterModelConfig = new CharacterModel.Config(CharacterModelConfig.Variant, CharacterModelConfig.Skin, newColor);
    }

    public void ChangePlayerName(string newName)
    {
        PlayerName = newName;
    }

    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();

        state.Add("PlayerName", PlayerName);
        state.Add("CharacterConfig", CharacterModelConfig);
        state.Add("MatchesPlayed", MatchesPlayed);

        return state;
    }

    public void RestoreState(object state)
    {
        Dictionary<string, object> restoredState = (Dictionary<string, object>)state;

        PlayerName = (string)restoredState["PlayerName"];
        CharacterModelConfig = (CharacterModel.Config)restoredState["CharacterConfig"];
        MatchesPlayed = (int)restoredState["MatchesPlayed"];
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
        //CharacterModelConfig = new CharacterModel.Config();
        MatchesPlayed = 0;
        Level.ResetPlayer();
        Currency.ResetPlayer();
    }

    public void MatchComplete()
    {
        MatchesPlayed++;
    }

    public void ResetPlayer()
    {
        ClearPlayer();
        Game.SaveSystem.SaveGameFile();
    }
}
