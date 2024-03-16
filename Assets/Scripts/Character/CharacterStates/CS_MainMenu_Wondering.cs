using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MainMenu_Wondering : CharacterState
{
    MainMenuMatch match => (MainMenuMatch)Game.Match;

    Vector3 targetPosition = Vector3.zero;
    float distanceProximity = 1.1f;
    Vector3 lastPos;

    public CS_MainMenu_Wondering(Character character) : base(character)
    {
    }

    public override void StateStart()
    {
        character.Animator.CrossFade("ScavangerHunt_Locomotion", 0.1f);
        character.NavMeshAgent.speed = match.CharacterSpeed;
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

        if (targetPosition == Vector3.zero)
        {
            Vector3 newPosition = match.PlayersCharacters.NextSpawnPoint;
            if (newPosition != new Vector3()) targetPosition = newPosition;
        }
        else if (character.NavMeshAgent.destination != targetPosition)
        {
            //Debug.Log(character.name + " setting NavMeshAgent destinationt to = " + targetPosition.ToString("F2"));
            character.NavMeshAgent.SetDestination(targetPosition);
            targetPosition = character.NavMeshAgent.destination;
        }
        else if (distanceToTarget <= distanceProximity)
        {
            targetPosition = Vector3.zero;
        }
        //else Debug.Log(character.name + " distance to target = " + distanceToTarget.ToString("F2"));
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
    }

    private float distanceToTarget
    {
        get
        {
            return Vector3.Distance(targetPosition, character.transform.position);
        }
    }
}
