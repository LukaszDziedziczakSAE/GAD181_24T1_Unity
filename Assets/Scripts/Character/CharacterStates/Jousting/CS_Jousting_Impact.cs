using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Impact : CharacterState
{
    private JoustingMatch match;
    private Character other;
    private CS_Jousting_Riding riding;
    private int pointsToAward = 1;

    public CS_Jousting_Impact(Character character) : base(character)
    {
        match = (JoustingMatch)Game.Match;
    }

    public override void StateStart()
    {
        other = match.OtherCharacter(character);
        if (character.PlayerIndex == 1)
        {
            character.Animator.CrossFade("Rider_Death", 0.1f);
        }
        match.AwardPlayerPoints(other.PlayerIndex, pointsToAward);
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