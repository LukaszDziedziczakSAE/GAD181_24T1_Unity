using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Archering_Drawing : CharacterState
{
    private ArrowShootingMatch match;
    private UI_TargetShooting ui;
    float startingYPostition;
    public CS_Archering_Drawing(Character character) : base(character)
    {
        ui = (UI_TargetShooting)Game.UI;
        match = (ArrowShootingMatch)Game.Match;
    }

    public override void StateStart()
    {
        Game.InputReader.OnTouchReleased += InputReader_OnTouchReleased;
        ui.ArrowShootingIndicator.gameObject.SetActive(true);
        startingYPostition = Game.InputReader.TouchPosition.y; 
    }

    public override void Tick()
    {
        float currentYPosition= Game.InputReader.TouchPosition.y;
        float distance = startingYPostition - currentYPosition;
        if (distance < 0) 
        {
            distance = 0;        
        }
        ui.ArrowShootingIndicator.UpdateDrawDistance(distance);
    }
    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {
        Game.InputReader.OnTouchReleased -= InputReader_OnTouchReleased;
        ui.ArrowShootingIndicator.gameObject.SetActive(false);
        
    }

    private void InputReader_OnTouchReleased()
    {
        float currentYPosition = Game.InputReader.TouchPosition.y;
        float distance = startingYPostition - currentYPosition;
        if (distance < 0)
        {
            distance = 0;
        }
        if (distance >= match.MinimumDrawDistanceToFire)
        {
            character.SetNewState(new CS_Archering_Releasing(character,distance));
        }
        else
        {
            character.SetNewState(new CS_Archering_Standing(character));
        }


    }
}
