using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_PickUp : CharacterState
{
    ArrowSupply_Crate crate;

    ArrowSupply_Arrow arrow;

    public CS_ArrowSupply_PickUp(Character character, ArrowSupply_Crate crate) : base(character)
    {
        this.crate = crate;
    }

    public override void StateStart()
    {
        character.Animator.CrossFade("ScavangerHunt_Pickup", 0.1f);

        Debug.Log(character.PlayerIndex + " has entered the pickup state");
    }

    public override void Tick()
    {
        if (arrow == null)
        {
            //Debug.Log("No arrow");            
            return;
        }
        if (character.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Pickup") && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            character.SetNewState(new CS_ArrowSupply_Carrying(character, arrow));
        }
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
    }

    public void Grab()
    {
        arrow = crate.SpawnInCharactersHand(character);
    }
}
