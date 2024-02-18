using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMatch : MinigameMatch
{
    UI_MainMenu ui;


    public override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    public override void MatchStart()
    {
        ui = (UI_MainMenu)Game.UI;

        ui.OpenMainMenu();
        Game.UpdatePlayersCharacterModel();
    }
}
