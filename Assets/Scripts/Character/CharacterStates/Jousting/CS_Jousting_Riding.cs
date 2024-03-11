using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Riding : CharacterState
{
    private JoustingMatch match;
    private UI_Jousting ui;
    private Character other;
    public CS_Jousting_Riding(Character character) : base(character)
    {
        match = (JoustingMatch)Game.Match;
        ui = (UI_Jousting)Game.UI;
        other = match.OtherCharacter(character);
    }

    public override void StateStart()
    {
        ui.JoustingIndicator.gameObject.SetActive(true);
        character.Animator.CrossFade("Rider_Gallop", 0.1f);

        if (character.HorseAnimator != null)
        {
            character.HorseAnimator.CrossFade("LocomotionBlend", 0.1f);
            character.HorseAnimator.SetFloat("speed", 1);
        }

        
        /*if (character.PlayerIndex == 0)
        {
            ui.JoustingIndicator.gameObject.SetActive(true);
            character.Animator.CrossFade("Rider_Gallop", 0.1f);
        }

        else if (character.PlayerIndex == 1)
        {
            ui.EnemyJoustingIndicator.gameObject.SetActive(true);
            character.Animator.CrossFade("Rider_Gallop", 0.1f);
        }*/

        //Debug.Log("You've entered riding state");

        if (IsPlayerCharacter) Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
    }

    public override void Tick()
    {
        character.transform.position += character.transform.forward * match.HorseSpeed * Time.deltaTime;

        if (character.PlayerIndex == 0)
        {
            ui.JoustingIndicator.UpdateDistanceIndicator(Distance());
            ui.JoustingIndicator.UpdateStrikingDistanceIndicator(IsWithinJoustingDistance());
            ui.EndIndicator.UpdateEndIndicator(ReachedEnd());
            match.PlayerReachedEnd(character);
        }

        else if (character.PlayerIndex == 1)
        {
            ui.EnemyJoustingIndicator.UpdateDistanceIndicator(Distance());
            ui.EnemyJoustingIndicator.UpdateStrikingDistanceIndicator(IsWithinJoustingDistance());
        }

        /*if (Game.InputReader.TouchPressed && IsWithinJoustingDistance())
        {
            match.PlayerAttack(character);
        }*/


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
}
