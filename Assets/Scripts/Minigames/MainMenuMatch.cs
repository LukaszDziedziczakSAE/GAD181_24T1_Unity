using Cinemachine;
using QFSW.QC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMatch : MinigameMatch
{
    UI_MainMenu ui;
    [field: SerializeField] public CinemachineVirtualCamera MainMenuCamera {  get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera CharacterCamera { get; private set; }
    [field: SerializeField] public float CameraBlendTime { get; private set; } = 0.2f;
    [field: SerializeField] public MainMenuCharacters PlayersCharacters { get; private set; }
    [field: SerializeField] public float CharacterSpeed { get; private set; } = 0.8f;

    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected override void PrematchStart()
    {
        //base.PrematchStart();
        Mode = EState.inProgress;
    }

    protected override void MatchStart()
    {
        ui = (UI_MainMenu)Game.UI;

        Game.UpdatePlayersCharacterModel();
        //PlayersCharacters.SpawnCharacters();
        QuantumConsole.Instance.Deactivate();
        Game.CameraManager.SwitchTo(MainMenuCamera);
        ui.OpenMainMenu();
    }
}
