using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class CS_Archering_Drawing : CharacterState
{
    private ArrowShootingMatch match;
    private UI_TargetShooting ui;
    private TargetShooting_ShootArrow arrow;
    float startingYPostition;
    private float maxAngle = 45f;
    private float startingXPos;
    private float turnRatio = 5;

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

        startingXPos = Game.InputReader.TouchPosition.x;
    }

    public override void Tick()
    {
        if(character.PlayerIndex != 0)
        {
            return;
        }
        float distanceX = startingXPos - Game.InputReader.TouchPosition.x;
        float rotation = distanceX / turnRatio;
        if (rotation > maxAngle) rotation = maxAngle;
        else if (rotation < -maxAngle) rotation = -maxAngle;
        character.transform.eulerAngles = new UnityEngine.Vector3(character.transform.eulerAngles.x, rotation, character.transform.eulerAngles.z);

        float currentYPosition = Game.InputReader.TouchPosition.y;
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
            float powerValue = distance / match.MaximumDrawDistanceToFire;
            if (powerValue < 0) powerValue = 0;
            else if (powerValue > 1) powerValue = 1;
            character.SetNewState(new CS_Archering_Releasing(character, powerValue));
        }
        else
        {
            character.SetNewState(new CS_Archering_Standing(character));
        }


    }

    void CalculateLaunchPower()
    {
        float currentYPosition = Game.InputReader.TouchPosition.y;
        float distance = startingYPostition - currentYPosition;

        if (distance < 0)
        {
            distance = 0;
        }
        ui.ArrowShootingIndicator.UpdateDrawDistance(distance);
        float distanceNormalized = distance / match.MaximumDrawDistanceToFire;
        distanceNormalized = Mathf.Clamp(distanceNormalized, 0, 1);
    }
    
}
