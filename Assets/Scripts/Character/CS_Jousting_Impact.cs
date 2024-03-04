using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Impact : CharacterState
{
    public CS_Jousting_Impact(Character character) : base(character)
    {
    }

    public override void StateStart()
    {
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
