using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CS_ArrowSupply_Carrying : CharacterState
{
    ArrowSupply_Arrow arrow;
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;
    public ArrowSupply_Arrow Arrow => arrow;

    [SerializeField] private string targetTag = "Delivery";
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float idleDuration = 1f;

    private Vector3 lastPosition;
    private float idleTimer;

    public CS_ArrowSupply_Carrying(Character character, ArrowSupply_Arrow arrow) : base(character)
    {
        this.arrow = arrow;
    }

    public override void StateStart()
    {
        lastPosition = character.transform.position;

        if (!IsPlayerCharacter)
        {
            SetDestinationToDeliveryPoint();
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
                SetDestinationToDeliveryPoint();
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
        // Handle touch input for player character
    }

    private void SetDestinationToDeliveryPoint()
    {
        GameObject[] deliveryPoints = GameObject.FindGameObjectsWithTag(targetTag);
        if (deliveryPoints.Length == 0)
        {
            Debug.LogError("No objects with tag '" + targetTag + "' found.");
            return;
        }

        GameObject randomDeliveryPoint = deliveryPoints[Random.Range(0, deliveryPoints.Length)];

        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
        randomDirection += randomDeliveryPoint.transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, maxDistance, NavMesh.AllAreas);

        character.NavMeshAgent.SetDestination(navHit.position);
        character.NavMeshAgent.isStopped = false;
    }
}