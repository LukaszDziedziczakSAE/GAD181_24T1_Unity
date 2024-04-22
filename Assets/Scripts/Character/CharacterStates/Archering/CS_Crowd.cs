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
        float random = Random.Range(0.0f, 1.0f);
        if (character.PlayerIndex == -1) character.Animator.CrossFade("Podium_Cheering", 0.1f,0,random);
        if (character.PlayerIndex == -2) character.Animator.CrossFade("Podium_Clapping", 0.1f,0,random);        
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
