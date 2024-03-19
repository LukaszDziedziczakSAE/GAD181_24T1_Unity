using QFSW.QC.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Riding : CharacterState
{
    private JoustingMatch match;
    private Character other;
    private UI_Jousting ui;

    public CS_Jousting_Riding(Character character) : base(character)
    {
        match = (JoustingMatch)Game.Match;
        ui = (UI_Jousting)Game.UI;
        other = match.OtherCharacter(character);
    }

    public override void StateStart()
    {
        ui.JoustingIndicator.gameObject.SetActive(true);
        character.Animator.CrossFade("Jousting_Rider_Gallop", 0.1f);

        if (character.HorseAnimator != null)
        {
            character.HorseAnimator.CrossFade("LocomotionBlend", 0.1f);
            character.HorseAnimator.SetFloat("speed", 1);
        }

        match.horseSpeed = 2f;
    }

    public override void Tick()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, 1f);

        if (IsPlayerCharacter)
        {
            CheckForAttackInput();
        }

        if (other.State.GetType() == typeof(CS_Jousting_Impact))
        {
            character.SetNewState(new CS_Jousting_Idle(character));
            return;
        }

        if (character.PlayerIndex == 0)
        {
            character.transform.position += movementDirection.normalized * match.horseSpeed * Time.deltaTime;
            ui.JoustingIndicator.UpdateDistanceIndicator(Distance());
            ui.JoustingIndicator.UpdateStrikingDistanceIndicator(IsWithinJoustingDistance());
            ui.EndIndicator.UpdateEndIndicator(ReachedEnd());
            match.PlayerReachedEnd(character);
          
            if (horizontalInput == 0)
            {
                //character.transform.position += movementDirection.normalized * match.HorseSpeed * Time.deltaTime;
                character.Animator.CrossFade("Jousting_Rider_Gallop", 0.1f);
                character.HorseAnimator.CrossFade("LocomotionBlend", 0.1f);
                character.HorseAnimator.SetFloat("speed", 1);
            }

            else if (horizontalInput < 0) 
            {
                character.transform.Rotate(Vector3.up, -match.TurnSpeed * Time.deltaTime);
                character.Animator.CrossFade("Jousting_Rider_Left", 0.1f);
                //character.HorseAnimator.CrossFade("Jousting_Horse_Left", 0.1f);
            }
            else if (horizontalInput > 0) 
            {
                character.transform.Rotate(Vector3.up, match.TurnSpeed * Time.deltaTime);
                character.Animator.CrossFade("Jousting_Rider_Right", 0.1f);
                //character.Animator.CrossFade("Jousting_Horse_Right", 0.1f);
            }
        }
        else if (character.PlayerIndex == 1)
        {
            character.transform.position -= movementDirection.normalized * match.horseSpeed * Time.deltaTime;
            ui.EnemyJoustingIndicator.UpdateDistanceIndicator(Distance());
            ui.EnemyJoustingIndicator.UpdateStrikingDistanceIndicator(IsWithinJoustingDistance());
        }
    }

    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {
        if (character.PlayerIndex == 0)
        {
            ui.JoustingIndicator.gameObject.SetActive(false);
        }

        else if (character.PlayerIndex == 1)
        {
            ui.EnemyJoustingIndicator.gameObject.SetActive(false);
        }

        if (IsPlayerCharacter) Game.InputReader.OnTouchPressed -= InputReader_OnTouchPressed;
    }

    private float Distance()
    {
        float distance = 0;

        if (character.PlayerIndex == 0)
        {
            distance = other.transform.position.z - character.transform.position.z;
        }

        else if (character.PlayerIndex == 1)
        {
            distance = character.transform.position.z - other.transform.position.z;
        }

        if (distance < 0)
        {
            distance = 0;
        }

        return distance;
    }

    public bool IsWithinJoustingDistance()
    {
        float distance = Distance();
        return distance >= match.MinimumJoustingDistance && distance <= match.MaximumJoustingDistance;
    }

    private float PlayerPosition()
    {
        float position = 0;

        if (character.PlayerIndex == 0)
        {
            position = character.transform.position.z;
        }

        return position;
    }

    public bool ReachedEnd()
    {
        float position = PlayerPosition();
        return position >= match.EndDistance;
    }

    private void InputReader_OnTouchPressed()
    {
        character.SetNewState(new CS_Jousting_Attack(character));
    }

    private void CheckForAttackInput()
    {
        if (Input.GetMouseButtonDown(0)) // Change this according to your input method
        {
            character.SetNewState(new CS_Jousting_Attack(character));
        }
    }
}