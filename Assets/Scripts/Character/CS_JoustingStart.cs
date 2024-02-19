using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_JoustingStart : CharacterState
{
    public CS_JoustingStart(Character character) : base(character)
    {
    }

    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {

    }

    public override void StateStart()
    {
        Debug.Log("Test state working!");
    }
    public override void Tick()
    {

    }
}
