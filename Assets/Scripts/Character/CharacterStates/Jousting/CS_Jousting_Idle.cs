using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Idle : CharacterState
{
    //private UI_Jousting ui;

    public CS_Jousting_Idle(Character character) : base(character)
    {
        //ui = (UI_Jousting)Game.UI;
    }

    public override void StateStart()
    {
        //if (ui.EndIndicator != null) ui.EndIndicator.UpdateEndIndicator(true);
        character.Animator.CrossFade("Jousting_Rider_Idle", 0.1f);
        character.HorseAnimator.CrossFade("Jousting_Horse_Idle", 0.1f);
        //Debug.Log("Player in idle state");
    }

    public override void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.SetNewState(new CS_Jousting_Riding(character));
        }

    }

    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {

    }
}