using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Impact : CharacterState
{
    private JoustingMatch match;
    private Character other;
    private CS_Jousting_Riding riding;

    public CS_Jousting_Impact(Character character) : base(character)
    {
        match = (JoustingMatch)Game.Match;
        other = match.OtherCharacter(character.PlayerIndex);
    }

    public override void StateStart()
    {
        if (character.PlayerIndex == 1)
        {
            character.Animator.CrossFade("Rider_Death", 0.1f);
        }

        Debug.Log("You've entered impact state");
    }

    public override void Tick()
    {
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
    }
}