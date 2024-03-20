using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CS_ArrowSupply_Locomotion : CharacterState
{
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;

    [SerializeField] private string targetTag = "Pickup";
    [SerializeField] private float maxDistance = .5f;
    [SerializeField] private float idleDuration = 1.5f;

    private Vector3 lastPosition;
    private float idleTimer;

    public CS_ArrowSupply_Locomotion(Character character) : base(character)
    {
    }

    public override void StateStart()
    {
        lastPosition = character.transform.position;

        Debug.Log(character.PlayerIndex + " has entered the locomotion state");

        if (!IsPlayerCharacter)
        {
            SetDestinationAroundRandomObject();
        }
        else
        {
            Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
            character.Animator.CrossFade("ScavangerHunt_Locomotion", 0.1f);
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

        // Check if the character is idle
        if (!IsPlayerCharacter && character.transform.position == lastPosition)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                SetDestinationAroundRandomObject();
                idleTimer = 0f;
            }
        }
        else
        {
            idleTimer = 0f;
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

    private void SetDestinationAroundRandomObject()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length == 0)
        {
            Debug.LogError("No objects with tag '" + targetTag + "' found.");
            return;
        }

        GameObject randomTarget = targets[Random.Range(0, targets.Length)];

        Vector3 randomDirection = Random.insideUnitSphere * randomTarget.GetComponent<SphereCollider>().radius;
        randomDirection += randomTarget.transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, maxDistance, NavMesh.AllAreas);

        character.NavMeshAgent.SetDestination(navHit.position);
        character.NavMeshAgent.isStopped = false;
    }
}
