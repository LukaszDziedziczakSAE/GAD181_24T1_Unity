using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ScavangerPickUp : CharacterState
{
    ScavangerHunt_PickUp pickUpObject;
    bool awardComplete;
    ScavangerHuntMatch match => (ScavangerHuntMatch)Game.Match;

    public CS_ScavangerPickUp(Character character, ScavangerHunt_PickUp pickUp) : base(character)
    {
        pickUpObject = pickUp;
    }

    public override void StateStart()
    {
        character.Animator.CrossFade("ScavangerHunt_Pickup", 0.1f);
        character.NavMeshAgent.isStopped = true;
        character.transform.LookAt(pickUpObject.transform.position);
    }

    public override void Tick()
    {
        if (character.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Pickup") && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            character.SetNewState(new CS_ScavangerLocomotion(character));
        }
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
        character.NavMeshAgent.isStopped = false;
        character.NavMeshAgent.destination = character.transform.position;
    }

    public void Grab()
    {
        pickUpObject.transform.parent = character.RightHand.transform;
        pickUpObject.transform.localPosition = Vector3.zero;
        pickUpObject.PlayPickUpSound();
    }

    public void GrabComplete()
    {
        if (awardComplete) return;
        awardComplete = true;
        match.AwardPlayerPoints(character.PlayerIndex, pickUpObject.Award);
        pickUpObject.CompletePickUp();
        if (match.CharacterWon(character)) match.Mode = MinigameMatch.EState.postMatch;
    }
}
