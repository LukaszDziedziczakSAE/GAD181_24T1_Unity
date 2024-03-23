using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_Delivery : CharacterState
{
    ArrowSupply_Arrow arrow;
    ArrowSupply_ArcherSupply archerSupply;
    private ArrowSupply_AIStateHolder stateHolder;

    public CS_ArrowSupply_Delivery(Character character, ArrowSupply_ArcherSupply archerSupply, ArrowSupply_Arrow arrow) : base(character)
    {
        this.archerSupply = archerSupply;
        this.arrow = arrow;
    }

    public override void StateStart()
    {
        if (stateHolder == null)
        {
            stateHolder = GameObject.FindObjectOfType<ArrowSupply_AIStateHolder>();
        }

        if (!IsPlayerCharacter && character.PlayerIndex <= 4)
        {
            stateHolder.SetState(ArrowSupply_AIStateHolder.AIState.Idle);
        }
        character.Animator.SetFloat("speed", 0);
        archerSupply.GiveArrow(arrow);
        Debug.Log(character.PlayerIndex + " has entered the delivery state");
    }

    public override void Tick()
    {
        character.SetNewState(new CS_ArrowSupply_Locomotion(character));
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
        if (!IsPlayerCharacter && character.PlayerIndex <= 4)
        {
            stateHolder.SetState(ArrowSupply_AIStateHolder.AIState.Locomotion);
        }
    }
}
