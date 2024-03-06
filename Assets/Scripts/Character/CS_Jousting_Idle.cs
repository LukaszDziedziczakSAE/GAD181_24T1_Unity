using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Idle : CharacterState
{
    private UI_Jousting ui;

    public CS_Jousting_Idle(Character character) : base(character)
    {
        ui = (UI_Jousting)Game.UI;
    }

    public override void StateStart()
    {
        ui.EndIndicator.UpdateEndIndicator(true);
        Debug.Log("Player in idle state");
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

    public bool ReachedEnd()
    {
        return character.PlayerIndex == 0 && character.transform.position.z == 16f;
    }
}