using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CS_ArrowSupply_Carrying : CharacterState
{
    ArrowSupply_Arrow arrow;
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;
    public ArrowSupply_Arrow Arrow => arrow;

    public ArrowSupply_AIStateHolder stateHolder;

    private ArrowSupply_AI ai;

    public CS_ArrowSupply_Carrying(Character character, ArrowSupply_Arrow arrow) : base(character)
    {
        this.arrow = arrow;
        ai = character.GetComponentInChildren<ArrowSupply_AI>();
    }

    public override void StateStart()
    {
        if (stateHolder == null)
        {
            stateHolder = GameObject.FindObjectOfType<ArrowSupply_AIStateHolder>();
        }

        if (stateHolder != null)
        {
            if (!IsPlayerCharacter && character.PlayerIndex <= 4)
            {
                stateHolder.SetState(ArrowSupply_AIStateHolder.AIState.Carrying);
            }
            else
            {
                Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
            }
        }
        else
        {
            Debug.LogError("ArrowSupply_AIStateHolder not found in the scene!");
        }

        character.Animator.CrossFade("ScavangerHunt_Locomotion", 0.1f);

        if (ai != null)
        {
            ai.SetNewDestination();
        }
    }

    public override void Tick()
    {
        if (character.NavMeshAgent.velocity.magnitude > 0)
        {
            character.Animator.SetFloat("speed", 1);
        }
        else
        {
            character.Animator.SetFloat("speed", 0);
        }
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

}