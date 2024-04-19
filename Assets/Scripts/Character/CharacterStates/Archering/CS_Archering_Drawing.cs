using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class CS_Archering_Drawing : CharacterState
{
    private ArrowShootingMatch match;
    private UI_TargetShooting ui;
    private TargetShooting_ShootArrow arrow;
    float startingYPostition;
    private float maxAngle = 45f;
    private float startingXPos;
    private float turnRatio = 20;
    private float rotationDeadzone = 5f;
    float characterStartingRotation;

    float currentYPosition => Game.InputReader.TouchPosition.y;
    float distanceY
    {
        get
        {
            float distance;
            if (currentYPosition == 0) distance = startingYPostition - lastYPostion;
            else distance = startingYPostition - currentYPosition;
            if (distance < 0) distance = 0;
            return distance;
        }
    }
    float powerValue
    {
        get
        {
            float power = distanceY / match.MaximumDrawDistanceToFire;
            power = Mathf.Clamp(power, 0, 1);
            return power;
        }
    }

    float lastYPostion;

    public CS_Archering_Drawing(Character character) : base(character)
    {
        ui = (UI_TargetShooting)Game.UI;
        match = (ArrowShootingMatch)Game.Match;
        
    }
    
    
    public override void StateStart()
    {
        
        if (IsPlayerCharacter) Game.InputReader.OnTouchReleased += InputReader_OnTouchReleased;
        if (match.DisplayDebugs)ui.ArrowShootingIndicator.gameObject.SetActive(true);
        ui.PowerSlider.gameObject.SetActive(true);
        startingYPostition = Game.InputReader.TouchPosition.y;        
        character.Animator.CrossFade("TargetShooting_DrawBlend", 0.1f);

        startingXPos = Game.InputReader.TouchPosition.x;
        characterStartingRotation = character.transform.eulerAngles.y;
        if (characterStartingRotation > 180)
        {
            characterStartingRotation -= 360;
        }
    }

    public override void Tick()
    {
        if (!IsPlayerCharacter) return;

        float distanceX = startingXPos - Game.InputReader.TouchPosition.x;
        if (distanceX > rotationDeadzone || distanceX < -rotationDeadzone)
        {
            float rotation = distanceX / turnRatio;
            rotation += characterStartingRotation;
            if (rotation > maxAngle) rotation = maxAngle;
            if (rotation < -maxAngle) rotation = -maxAngle;
            character.transform.eulerAngles = new UnityEngine.Vector3(character.transform.eulerAngles.x, rotation, character.transform.eulerAngles.z);
        }

        //ui.ArrowShootingIndicator.UpdateDrawDistance(startingYPostition, currentYPosition, distanceY, powerValue);
        //ui.ArrowShootingIndicator.UpdateBackgroundColour(distanceY >= match.MinimumDrawDistanceToFire);
        ui.PowerSlider.UpdateArrowPower(powerValue);

        character.Animator.SetFloat("DrawStrength", powerValue);

        if (currentYPosition != 0) lastYPostion = currentYPosition;

        //Debug.Log("startingYPostition=" + startingYPostition + ", currentYPosition=" + currentYPosition + ", currentYPosition=" + currentYPosition + ", distanceY=" + distanceY);
    }
    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {

        if (IsPlayerCharacter) Game.InputReader.OnTouchReleased -= InputReader_OnTouchReleased;
        ui.ArrowShootingIndicator.gameObject.SetActive(false);
        ui.PowerSlider.gameObject.SetActive(false);

    }

    private void InputReader_OnTouchReleased()
    {
        Debug.Log("END: startingYPostition=" + startingYPostition + ", currentYPosition=" + currentYPosition + ", currentYPosition=" + currentYPosition + ", distanceY=" + distanceY);

        if (distanceY >= match.MinimumDrawDistanceToFire)
        {
            Debug.Log(character.name + ": releasing arrow with " + (powerValue * 100).ToString("F0") + "% power");
            character.SetNewState(new CS_Archering_Releasing(character, powerValue));
        }
        else
        {
            character.SetNewState(new CS_Archering_Standing(character));
        }
    }
}
