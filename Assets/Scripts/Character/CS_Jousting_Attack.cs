using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Attack : CharacterState
{
    public CS_Jousting_Attack(Character character) : base(character)
    {
    }

    public override void StateStart()
    {
        Debug.Log("You've entered attack state");
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
