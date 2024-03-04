using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShootingMatch : MinigameMatch
{

    [field: SerializeField] public float MinimumDrawDistanceToFire { get; private set; }
    [field: SerializeField] public float MaximumDrawDistanceToFire { get; private set; }
    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected override void MatchStart()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Archering_Standing(character));
        }
    }

}
