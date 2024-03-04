using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoustingMatch : MinigameMatch
{
    [field: SerializeField] public float HorseSpeed { get; private set; }
    [field: SerializeField] public float MinimumJoustingDistance { get; private set; }
    [field: SerializeField] public float MaximumJoustingDistance { get; private set; }

    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected override void MatchStart()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Jousting_Riding(character));
        }
    }

    public Character OtherCharacter(int playerIndex)
    {
        if (playerIndex == 0)
        {
            return Game.CharacterByIndex(1);
        }
        else if (playerIndex == 1)
        {
            return Game.CharacterByIndex(0);
        }

        return null;
    }
}
