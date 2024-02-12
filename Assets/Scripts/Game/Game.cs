using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    [SerializeField] InputReader input;
    [SerializeField] Player player;
    [SerializeField] Character playerCharacter;
    public static InputReader InputReader => Instance.input;
    public static Player Player => Instance.player;
    public static Character PlayerCharacter => Instance.playerCharacter;
    public static MinigameMatch Match {  get; private set; }
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

    public void LoadSceneMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("Bootstrap Loaded");
        if (level == 0) return;

        player = FindAnyObjectByType<Player>();
        UI = FindAnyObjectByType<UI_Main>();
        if (player == null) return;
        playerCharacter = FindPlayersCharacter();
        playerCharacter?.Model.SetNewConfig(player.CharacterConfig);


        if (level == 1) Debug.Log("Main Menu loaded");
        else Debug.Log("Level " + level + " loaded");
    }

    private static Character FindPlayersCharacter()
    {
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character character in characters)
        {
            if (character.PlayerIndex == 0) return character;
        }
        return null;
    }



}
