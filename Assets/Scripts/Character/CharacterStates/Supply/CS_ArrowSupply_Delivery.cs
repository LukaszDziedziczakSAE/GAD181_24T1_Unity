using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_Delivery : CharacterState
{
    ArrowSupply_Arrow arrow;
    ArrowSupply_ArcherSupply archerSupply;

    public CS_ArrowSupply_Delivery(Character character, ArrowSupply_ArcherSupply archerSupply, ArrowSupply_Arrow arrow) : base(character)
    {
        this.archerSupply = archerSupply;
        this.arrow = arrow;
    }

    public override void StateStart()
    {
        character.Animator.SetFloat("speed", 0);
        archerSupply.GiveArrow(arrow);
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
