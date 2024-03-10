using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_StandingIdle : CharacterState
{
    public CS_StandingIdle(Character character) : base(character)
    {
    }

    public override void StateStart()
    {
        character.Animator.CrossFade("StandingIdle", 0.1f);
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
