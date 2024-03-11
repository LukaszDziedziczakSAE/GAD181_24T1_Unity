using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_ArcherWaiting : CharacterState
{
    float fireRate = 1;
    float stateStartTime;
    float timePassed => stateStartTime - Time.time;
    ArrowSupply_ArcherSupply archerSupply;

    public CS_ArrowSupply_ArcherWaiting(Character character) : base(character)
    {
        character.Animator.CrossFade("StandingIdle", 0.1f);
        
    }

    public override void StateStart()
    {
        archerSupply = character.GetComponentInChildren<ArrowSupply_ArcherSupply>();
        stateStartTime = Time.time;
        if (archerSupply == null) Debug.LogError("Missing Archer Supply Referance");
    }

    public override void Tick()
    {
        if (/*timePassed >= fireRate && */archerSupply.HasArrows)
        {
            character.SetNewState(new CS_ArrowSupply_ArcherFiring(character, archerSupply.TakeArrow()));
        }

        //Debug.Log("Archer has " + archerSupply.Arrows.Count + " arrows");
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
    }
}
