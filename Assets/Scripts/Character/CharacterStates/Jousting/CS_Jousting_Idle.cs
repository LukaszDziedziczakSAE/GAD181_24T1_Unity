using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Idle : CharacterState
{
    private UI_Jousting ui;
    private JoustingMatch match;

    public CS_Jousting_Idle(Character character) : base(character)
    {
        ui = (UI_Jousting)Game.UI;
        match = (JoustingMatch)Game.Match;
    }

    public override void StateStart()
    {
        ui.EndIndicator.UpdateEndIndicator(true);
        character.Animator.CrossFade("Jousting_Rider_Idle", 0.1f);
        character.HorseAnimator.CrossFade("Jousting_Horse_Idle", 0.1f);
        match.horseSpeed = 0f; // Set horse speed to 0 when in idle state
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