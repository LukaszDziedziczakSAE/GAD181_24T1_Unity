using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    [SerializeField] CameraManager cameraManager;
    [SerializeField] InputReader input;
    [SerializeField] Player player;
    [SerializeField] Character playerCharacter;
    [SerializeField] Sound_UI sound;
    public static InputReader InputReader => Instance.input;
    public static Player Player => Instance.player;
    public static Character PlayerCharacter => Instance.playerCharacter;
    public static MinigameMatch Match { get; private set; }
    public static UI_Main UI { get; private set; }
    public static SaveSystem SaveSystem { get; private set; }
    public static CameraManager CameraManager => Instance.cameraManager;
    public static Sound_UI Sound => Instance.sound;

    private void Awake()
    {
        SaveSystem = GetComponent<SaveSystem>();
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        DontDestroyOnLoad(this);
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        int level = scene.buildIndex;

        if (level == 0)
        {
            Debug.Log("Bootstrap Loaded");
            return;
        }

        cameraManager.OnSceneLoad();
        player = FindAnyObjectByType<Player>();

        UI = FindAnyObjectByType<UI_Main>();
        if (UI != null) UI.LevelLoaded();

        if (player == null) return;
        playerCharacter = FindPlayersCharacter();
        UpdatePlayersCharacterModel();

        if (level == 1) Debug.Log("Main Menu loaded");
        else Debug.Log("Level " + level + " loaded");

        Match = FindObjectOfType<MinigameMatch>();
        if (Match != null) Match.Mode = MinigameMatch.EState.preMatch;
    }

    private void Start()
    {
        if (SaveSystem.SaveFileExists) SaveSystem.LoadGameFile();
        else SaveSystem.SaveGameFile();
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    /*private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            Debug.Log("Bootstrap Loaded");
            return;
        }

        cameraManager.OnSceneLoad();
        player = FindAnyObjectByType<Player>();

        UI = FindAnyObjectByType<UI_Main>();
        if (UI != null) UI.LevelLoaded();

        if (player == null) return;
        playerCharacter = FindPlayersCharacter();
        UpdatePlayersCharacterModel();

        if (level == 1) Debug.Log("Main Menu loaded");
        else Debug.Log("Level " + level + " loaded");

        Match = FindObjectOfType<MinigameMatch>();
        if (Match != null) Match.Mode = MinigameMatch.EState.preMatch;
    }*/

    public static void UpdatePlayersCharacterModel()
    {
        if (Instance.player.PlayerName == null || Instance.player.PlayerName == "")
            Instance.playerCharacter?.Model.HideAllModels();
        else Instance.playerCharacter?.Model.SetNewConfig(Instance.player.CharacterConfig);
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
}
