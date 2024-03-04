using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Archering_Releasing : CharacterState
{
    float drawPower;
    public CS_Archering_Releasing(Character character, float drawPower) : base(character)
    {
        this.drawPower = drawPower;
    }

    public override void StateStart()
    {
        character.SetNewState(new CS_Archering_Standing(character));
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
