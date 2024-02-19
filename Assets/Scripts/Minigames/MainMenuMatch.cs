using QFSW.QC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMatch : MinigameMatch
{
    UI_MainMenu ui;


    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected override void MatchStart()
    {
        ui = (UI_MainMenu)Game.UI;

        ui.OpenMainMenu();
        Game.UpdatePlayersCharacterModel();
        QuantumConsole.Instance.Deactivate();
    }
}
