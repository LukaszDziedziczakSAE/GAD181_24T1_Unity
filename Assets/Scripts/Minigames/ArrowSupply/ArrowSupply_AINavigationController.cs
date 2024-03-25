using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ArrowSupply_AINavigationController : AI
{
    public NavMeshAgent agent;

    public GameObject[] pickupLocations;

    public GameObject[] deliveryLocations;

    private GameObject currentTarget; // To keep track of the current target location

    //private bool isMoving = false;

    public bool carryingArrow = false; // Indicates whether the AI is currently carrying an arrow

    private Type Carrying = new CS_ArrowSupply_Carrying(null, null).GetType();

    private bool isMoving => character.NavMeshAgent.velocity.magnitude > 0;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        SetNewDestination(); // Set initial destination
    }

    void Update()
    {
        // Always trigger movement logic regardless of state
        if (!isMoving)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        if (character == null) Debug.LogError("missing character referance");

        if (!isMoving)
        {
            Debug.Log(name + ": State = " + character.State);
            GameObject[] locations = pickupLocations; // Default to pickup locations

            if (character.State.GetType() == Carrying)
            {
                locations = deliveryLocations; // Change to delivery locations if carrying arrow
            }

            if (locations.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, locations.Length);
                currentTarget = locations[index];

                agent.SetDestination(currentTarget.transform.position);

                agent.isStopped = false; // Allow the agent to move towards the new destination

                //isMoving = true; // Set moving flag

                // Debugging
                Debug.Log($"New destination set to: {currentTarget.name} at {currentTarget.transform.position}");

                if (agent.pathStatus != NavMeshPathStatus.PathComplete)
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