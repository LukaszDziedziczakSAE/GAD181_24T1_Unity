using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Impact : CharacterState
{
    private JoustingMatch match;
    private Character other;
    private int pointsToAward = 1;

    public CS_Jousting_Impact(Character character) : base(character)
    {
        match = (JoustingMatch)Game.Match;
    }

    public override void StateStart()
    {
        character.Sounds.PlayGruntSound();

        character.Animator.CrossFade("Jousting_Rider_Death", 0.1f);

        other = match.OtherCharacter(character);
        match.AwardPlayerPoints(other.PlayerIndex, pointsToAward);
        Debug.Log("You've entered impact state");
    }

    public override void Tick()
    {
        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            character.SetNewState(new CS_Jousting_Idle(character));
        }
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
    }
}