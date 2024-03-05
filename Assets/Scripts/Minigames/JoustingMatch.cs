using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoustingMatch : MinigameMatch
{
    [field: SerializeField] public float HorseSpeed { get; private set; }
    [field: SerializeField] public float MinimumJoustingDistance { get; private set; }
    [field: SerializeField] public float MaximumJoustingDistance { get; private set; }
    [field: SerializeField] public float EndDistance { get; private set; }

    private CS_Jousting_Riding riding;

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

    //public void PlayerReachedEnd()
    //{
    //    foreach(Character character in Compeditors)
    //    {
    //        if (character.PlayerIndex == 0 && character.transform.position.z == 16)
    //        {
    //            character.SetNewState(new CS_Jousting_Idle(character));
    //            Debug.Log("Player Reached End");
    //        }
    //    }
    //}    
}
