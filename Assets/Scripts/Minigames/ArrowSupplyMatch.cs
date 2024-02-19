using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupplyMatch : MinigameMatch
{
    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected override void MatchStart()
    {
        foreach(Character character in Compeditors) 
        {
            character.SetNewState(new CS_ArrowSupply_States(character));
        }
    }
}
