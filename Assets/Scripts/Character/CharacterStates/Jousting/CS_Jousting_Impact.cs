using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Impact : CharacterState
{
    private JoustingMatch match;
    private Character other;
    private CS_Jousting_Riding riding;
    public bool impactState = false;
    

    public CS_Jousting_Impact(Character character) : base(character)
    {
        match = (JoustingMatch)Game.Match;
    }

    public override void StateStart()
    {
        character.Sounds.PlayGruntSound();

        other = match.OtherCharacter(character);
        if (character.PlayerIndex == 1 && character.HorseAnimator != null)
        {
            character.Animator.CrossFade("Jousting_Rider_Death", 0.1f);
            character.HorseAnimator.CrossFade("Jousting_Horse_Death", 0.1f);
        }

        impactState = true;

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