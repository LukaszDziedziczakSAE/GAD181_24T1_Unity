using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_Archering_Drawing : CharacterState
{
    private ArrowShootingMatch match;
    private UI_TargetShooting ui;
    private TargetShooting_ShootArrow arrow;
    float startingYPostition;
    public CS_Archering_Drawing(Character character) : base(character)
    {
        ui = (UI_TargetShooting)Game.UI;
        match = (ArrowShootingMatch)Game.Match;
        
    }
    
    
    public override void StateStart()
    {
        if (IsPlayerCharacter) Game.InputReader.OnTouchReleased += InputReader_OnTouchReleased;
        ui.ArrowShootingIndicator.gameObject.SetActive(true);
        startingYPostition = Game.InputReader.TouchPosition.y;        
        character.Animator.CrossFade("TargetShooting_DrawBlend", 0.1f);
    }

    public override void Tick()
    {
        if(character.PlayerIndex != 0)
        {
            return;
        }
        float currentYPosition= Game.InputReader.TouchPosition.y;
        float distance = startingYPostition - currentYPosition;
        if (distance < 0) 
        {
            distance = 0;        
        }
        ui.ArrowShootingIndicator.UpdateDrawDistance(distance);
        float distanceNormalized = distance / match.MaximumDrawDistanceToFire;
        distanceNormalized = Mathf.Clamp(distanceNormalized, 0, 1);
        //arrow.ShootArrow(distanceNomalized)
        
        character.Animator.SetFloat("DrawStrength", distanceNormalized);
    }
    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {
        if (IsPlayerCharacter) Game.InputReader.OnTouchReleased -= InputReader_OnTouchReleased;
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