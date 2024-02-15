using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    [SerializeField] InputReader input;
    [SerializeField] Player player;
    [SerializeField] Character playerCharacter;
    public static InputReader InputReader => Instance.input;
    public static Player Player => Instance.player;
    public static Character PlayerCharacter => Instance.playerCharacter;
    public static MinigameMatch Match { get; private set; }
    public static UI_Main UI { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //PlayerCharacter.Model.SetNewConfig(Player.CharacterConfig);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            Debug.Log("Bootstrap Loaded");
            return;
        }

        player = FindAnyObjectByType<Player>();
        UI = FindAnyObjectByType<UI_Main>();
        if (player == null) return;
        playerCharacter = FindPlayersCharacter();
        playerCharacter?.Model.SetNewConfig(player.CharacterConfig);

        if (level == 1) Debug.Log("Main Menu loaded");
        else Debug.Log("Level " + level + " loaded");

        Match = FindObjectOfType<MinigameMatch>();
        if (Match != null) Match.MatchStart();
    }

    private static Character FindPlayersCharacter()
    {
        Character[] characters = FindObjectsOfType<Character>();
        //Debug.Log("found " + characters.Length + " characters");
        foreach (Character character in characters)
        {
            if (character.PlayerIndex == 0) return character;
        }
        return null;
    }

    public static void CreateCharacterConfigs()
    {
        foreach (CharacterConfig character in PlayerCharacter.Model.CharacterConfigs)
        {
            AssetDatabase.CreateAsset(character, "Assets/Prefabs/Characters/" + character.name + ".asset");
            AssetDatabase.SaveAssets();
        }
    }

    public static CharacterConfig[] ConfigsUnlockedAt(int level)
    {
        CharacterConfig[] configs = PlayerCharacter.Model.Configs;
        List<CharacterConfig> configsAtLevel = new List<CharacterConfig>();
        foreach (CharacterConfig character in configs)
        {
            if (character.UnlockLevel == level) configsAtLevel.Add(character);
        }

        return configsAtLevel.ToArray();
    }

    public static int HighestLevel
    {
        get
        {
            CharacterConfig[] configs = PlayerCharacter.Model.Configs;
            int highestLevel = 0;
            foreach (CharacterConfig character in configs)
            {
                if (character.UnlockLevel > highestLevel) highestLevel = character.UnlockLevel;

            }

            return highestLevel;
        }
    }

    public static void LoadPortraitMaker()
    {
        SceneManager.LoadScene("PortraitMaker");
    }

    public static Texture Portrait(string name)
    {
        return AssetDatabase.LoadAssetAtPath<Texture>("Assets/Textures/" + name + ".png");
    }
}