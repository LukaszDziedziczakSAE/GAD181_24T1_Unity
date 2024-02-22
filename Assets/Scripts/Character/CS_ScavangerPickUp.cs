using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ScavangerPickUp : CharacterState
{
    PickUp pickUpObject;

    public CS_ScavangerPickUp(Character character, PickUp pickUp) : base(character)
    {
        pickUpObject = pickUp;
    }

    public override void StateStart()
    {
        Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
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

    private void InputReader_OnTouchPressed()
    {

    }

    public void Grab()
    {
        pickUpObject.transform.parent = character.RightHand.transform;
        pickUpObject.transform.localPosition = Vector3.zero;
    }

    public void GrabComplete()
    {
        pickUpObject.CompletePickUp();
    }
}
