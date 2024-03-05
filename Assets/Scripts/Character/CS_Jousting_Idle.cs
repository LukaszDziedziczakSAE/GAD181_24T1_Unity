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
        Debug.Log("Player in idle state");
    }

    public override void Tick()
    {
        character.transform.position += character.transform.forward * 0 * Time.deltaTime;

        ui.EndIndicator.UpdateEndIndicator(ReachedEnd());
    }

    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {

    }

    public bool ReachedEnd()
    {
        return character.PlayerIndex == 0 && character.transform.position.z == 16;
    }
}