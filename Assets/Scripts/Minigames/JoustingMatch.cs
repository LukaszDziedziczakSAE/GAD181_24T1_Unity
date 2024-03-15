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
    [field: SerializeField] public float TurnSpeed { get; private set; }

    private Jousting_Weapon weapon;



    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected override void MatchStart()
    {
       weapon = FindObjectOfType<Jousting_Weapon>();
        
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Jousting_Riding(character));
        }
    }

    public Character OtherCharacter(Character character)
    {
        if (character == null) Debug.LogError("missing character referance");

        if (character.PlayerIndex == 0)
        {
            return Game.CharacterByIndex(0);
        }
        else if (character.PlayerIndex == 1)
        {
            return Game.CharacterByIndex(1);
        }

        return null;
    }

    public void PlayerReachedEnd(Character character)
    {
        if (character.transform.position.z >= EndDistance)
        {
            character.SetNewState(new CS_Jousting_Idle(character));
            //Debug.Log("Player Reached End");
        }
    }
}