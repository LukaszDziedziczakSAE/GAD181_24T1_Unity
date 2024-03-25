using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_Delivery : CharacterState
{
    ArrowSupply_Arrow arrow;

    ArrowSupply_ArcherSupply archerSupply;
       

    private ArrowSupply_AI ai;

    public CS_ArrowSupply_Delivery(Character character, ArrowSupply_ArcherSupply archerSupply, ArrowSupply_Arrow arrow) : base(character)
    {
        this.archerSupply = archerSupply;

        this.arrow = arrow;

        ai = character.GetComponentInChildren<ArrowSupply_AI>();
    }

    public override void StateStart()
    {
        Game.PlayerCharacter.NavMeshAgent.isStopped = true;

        character.Animator.SetFloat("speed", 0);

        archerSupply.GiveArrow(arrow, character);

        Debug.Log(character.name + " has entered the delivery state");
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
       
    }
}
