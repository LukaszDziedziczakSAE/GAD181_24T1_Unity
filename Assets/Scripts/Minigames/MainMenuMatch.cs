using Cinemachine;
using QFSW.QC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMatch : MinigameMatch
{
    UI_MainMenu ui;
    [field: SerializeField] public CinemachineVirtualCamera[] MainMenuCameras {  get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera CharacterCamera { get; private set; }
    [field: SerializeField] public float CameraBlendTime { get; private set; } = 0.2f;
    [field: SerializeField] public MainMenuCharacters PlayersCharacters { get; private set; }
    [field: SerializeField] public float CharacterSpeed { get; private set; } = 0.8f;
    [SerializeField] float cameraSwitchTimer = 5f;

    int camIndex;
    float cameraTimer;

    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected override void PrematchStart()
    {
        Game.CameraManager.SetStartingCamera(MainMenuCameras[0]);
        cameraTimer = cameraSwitchTimer;
        Game.Music.PlayMainMenuTrack();
        Mode = EState.inProgress;
    }

    protected override void MatchStart()
    {
        ui = (UI_MainMenu)Game.UI;

        Game.UpdatePlayersCharacterModel();
        //PlayersCharacters.SpawnCharacters();
        QuantumConsole.Instance.Deactivate();
        //Game.CameraManager.SwitchTo(MainMenuCamera);
        
        //ui.OpenMainMenu();
        ui.ShowMainMenuStatus();
    }

    protected override void MatchTick()
    {
        base.MatchTick();

        cameraTimer -= Time.deltaTime;
        if (cameraTimer <= 0) SwitchMainMenuCamera();
    }

    private void SwitchMainMenuCamera()
    {
        int newIndex = camIndex;
        while (newIndex == camIndex)
        {
            newIndex = Random.Range(0, MainMenuCameras.Length);
        }
        camIndex = newIndex;

        Game.CameraManager.SwitchTo(MainMenuCameras[camIndex]);
        cameraTimer = cameraSwitchTimer;
    }
}
