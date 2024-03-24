using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArcheringState : CharacterState
{
    //have static variable for rotation here

    public CS_ArcheringState(Character character) : base(character)
    {
    }

    public override void StateStart()
    {
        Debug.Log("You've entered archery state");
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
