using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Crowd : CharacterState
{

    ArrowShootingMatch match => (ArrowShootingMatch)Game.Match;
    public CS_Crowd(Character character) : base(character)
    {

    }

    public override void StateStart()
    {
        if (Game.Match.MatchTime >= -0.5f)
        {
            if (character.PlayerIndex == 103) character.Animator.CrossFade("Podium_Cheering", 0.1f);
            if (character.PlayerIndex == 104) character.Animator.CrossFade("Podium_Clapping", 0.1f);
        }

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
