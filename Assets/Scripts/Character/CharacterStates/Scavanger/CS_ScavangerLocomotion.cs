using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ScavangerLocomotion : CharacterState
{
    private ScavangerHuntMatch match => (ScavangerHuntMatch)Game.Match;

    public CS_ScavangerLocomotion(Character character) : base(character) { }

    Vector3 lastPosition;
    float distanceThreshold = 0.005f;
    float distanceToLastPostition
    {
        get
        {
            if (lastPosition == Vector3.zero) return 0;
            return Vector3.Distance(lastPosition, character.transform.position);
        }
    }

    public override void StateStart()
    {
        if (IsPlayerCharacter) Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
        character.Animator.CrossFade("ScavangerHunt_Locomotion", 0.1f);
    }

    public override void Tick()
    {
        if (distanceToLastPostition > distanceThreshold)
        {
            character.Animator.SetFloat("speed", 1);
        }

        else
        {
            character.Animator.SetFloat("speed", 0);
            character.Rigidbody.ResetInertiaTensor();
            character.Rigidbody.velocity = Vector3.zero;
        }

        lastPosition = character.transform.position;
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
        if (IsPlayerCharacter) Game.InputReader.OnTouchPressed -= InputReader_OnTouchPressed;
    }

    private void InputReader_OnTouchPressed()
    {
        

        RaycastHit raycastHit = Game.InputReader.RaycastFromTouchPoint;
        if (!raycastHit.Equals(new RaycastHit()))
        {
            match.ShowTouchIndicator(raycastHit.point);
            Game.PlayerCharacter.NavMeshAgent.SetDestination(raycastHit.point);
            Game.PlayerCharacter.NavMeshAgent.isStopped = false;
        }
    }
}
