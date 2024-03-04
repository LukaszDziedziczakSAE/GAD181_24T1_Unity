using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Archering_Standing : CharacterState
{
    public CS_Archering_Standing(Character character) : base(character)
    {
    }

    public override void StateStart()
    {
        Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
    }
    
    public override void Tick()
    {
        
    }
    public override void FixedTick()
    {
        
    }

    public override void StateEnd()
    {
        Game.InputReader.OnTouchPressed -= InputReader_OnTouchPressed;
    }


    private void InputReader_OnTouchPressed()
    {
        character.SetNewState(new CS_Archering_Drawing(character));
    }


}