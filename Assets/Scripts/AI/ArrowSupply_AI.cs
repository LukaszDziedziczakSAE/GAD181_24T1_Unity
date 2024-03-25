using MalbersAnimations.Controller;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ArrowSupply_AI : AI
{
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;

    private Transform currentTarget; // To keep track of the current target location

    //private bool isMoving = false;

    public bool carryingArrow = false; // Indicates whether the AI is currently carrying an arrow

    private bool isMoving => character.NavMeshAgent.velocity.magnitude > 0;

    void Start()
    {
        //SetNewDestination(); // Set initial destination
    }

    void Update()
    {
        /*// Always trigger movement logic regardless of state
        if (!isMoving)
        {
            SetNewDestination();
        }*/
    }

    public void SetNewDestination()
    {
        if (character == null) Debug.LogError("missing character referance");

        if (/*!isMoving*/ true)
        {
            Debug.LogWarning(character.name + " asking for new destination with State = " + character.State);

            Transform[] locations; /*= pickupLocations; // Default to pickup locations*/

            if (character.State.GetType() == new CS_ArrowSupply_Carrying(character, null).GetType())
            {
                locations = match.DeliveryLocations;
                Debug.Log("location is " + locations);
            }
            else if (character.State.GetType() == new CS_ArrowSupply_Locomotion(character).GetType())
            {
                locations = match.PickupLocations;
                Debug.Log("location is " + locations);
            }
            else
            {
                Debug.LogWarning(character.name + ": is in " + character.State.ToString());
                locations = new Transform[0];
            }

            if (locations.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, locations.Length);

                currentTarget = locations[index];

                character.NavMeshAgent.SetDestination(currentTarget.position);

                character.NavMeshAgent.isStopped = false; // Allow the agent to move towards the new destination

                //isMoving = true; // Set moving flag

                // Debugging
                Debug.Log($"New destination set to: {currentTarget.name} at {currentTarget.position}");

                if (character.NavMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
                {
                    Debug.LogWarning("NavMeshAgent cannot find a complete path to the destination.");
                }
            }
            else
            {
                Debug.LogWarning("No locations available.");
            }
        }
    }

    // Replace OnTriggerEnter with the method for detecting arrow pickup
    public void PickUpArrow()
    {
        // Pick up the arrow
        carryingArrow = true;
        Debug.Log("AI has picked up the arrow.");
        SetNewDestination(); // Set a new destination after delivering the arrow
    }

    // Call this method when the arrow is delivered
    public void DeliverArrow()
    {
        // Deliver the arrow
        carryingArrow = false;
        Debug.Log("AI has delivered the arrow.");
        SetNewDestination(); // Set a new destination after delivering the arrow
    }
}