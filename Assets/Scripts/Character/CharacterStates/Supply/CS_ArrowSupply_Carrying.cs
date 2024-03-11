using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_Carrying : CharacterState
{
    ArrowSupply_Arrow arrow;
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;
    public ArrowSupply_Arrow Arrow => arrow;

    public CS_ArrowSupply_Carrying(Character character, ArrowSupply_Arrow arrow) : base(character)
    {
        this.arrow = arrow;
    }

    public override void StateStart()
    {
        if (IsPlayerCharacter) Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
        character.Animator.CrossFade("ScavangerHunt_Locomotion", 0.1f);
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
        //Debug.Log("Touch Pressed");
        RaycastHit raycastHit = Game.InputReader.RaycastFromTouchPoint;
        if (!raycastHit.Equals(new RaycastHit()))
        {
            match.ShowTouchIndicator(raycastHit.point);
            Game.PlayerCharacter.NavMeshAgent.SetDestination(raycastHit.point);
            Game.PlayerCharacter.NavMeshAgent.isStopped = false;
        }
        //else Debug.LogWarning("No Hit");
    }
}
