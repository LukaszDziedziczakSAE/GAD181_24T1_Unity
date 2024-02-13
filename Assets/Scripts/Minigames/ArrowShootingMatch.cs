using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShootingMatch : MinigameMatch
{
    public override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    public override void MatchStart()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_ArcheringState(character));
        }
    }
}
