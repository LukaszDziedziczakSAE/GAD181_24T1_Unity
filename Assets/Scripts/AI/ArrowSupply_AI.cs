using System.Collections;
using System.Linq;
using UnityEngine;

public class ArrowSupply_AI : AI
{
    private Character characterAI;
    private CS_ArrowSupply_Locomotion locomotionState;
    private CS_ArrowSupply_PickUp pickUpState;
    private CS_ArrowSupply_Carrying carryingState;
    private CS_ArrowSupply_Delivery deliveryState;

    private ArrowSupply_Crate[] crates; // Array of available crates
    private Transform[] deliveryLocations; // Array of delivery locations

    private ArrowSupply_Crate currentCrate;
    private Transform currentDeliveryLocation;
    private ArrowSupply_Arrow carriedArrow;

    private enum AIState
    {
        Locomotion,
        PickUp,
        Carrying,
        Delivery
    }

    private AIState currentState;

    void Start()
    {
        characterAI = GetComponentInParent<Character>(); // Get the character component from the parent GameObject
        locomotionState = new CS_ArrowSupply_Locomotion(characterAI);
        pickUpState = new CS_ArrowSupply_PickUp(characterAI, currentCrate);
        carryingState = null;
        deliveryState = null;

        // Get all available crates and delivery locations
        crates = FindObjectsOfType<ArrowSupply_Crate>();
        deliveryLocations = GameObject.FindGameObjectsWithTag("Delivery").Select(obj => obj.transform).ToArray();

        // Initial state
        SetState(AIState.Locomotion);
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Locomotion:
                locomotionState.Tick();
                break;
            case AIState.PickUp:
                pickUpState.Tick();
                break;
            case AIState.Carrying:
                carryingState.Tick();
                break;
            case AIState.Delivery:
                deliveryState.Tick();
                break;
        }
    }

    private void SetState(AIState newState)
    {
        // Exit current state
        switch (currentState)
        {
            case AIState.Locomotion:
                locomotionState.StateEnd();
                break;
            case AIState.PickUp:
                pickUpState.StateEnd();
                break;
            case AIState.Carrying:
                carryingState.StateEnd();
                break;
            case AIState.Delivery:
                deliveryState.StateEnd();
                break;
        }

        // Enter new state
        switch (newState)
        {
            case AIState.Locomotion:
                currentState = AIState.Locomotion;
                locomotionState.StateStart();
                break;
            case AIState.PickUp:
                currentState = AIState.PickUp;
                // Choose a random crate
                currentCrate = crates[Random.Range(0, crates.Length)];
                pickUpState = new CS_ArrowSupply_PickUp(characterAI, currentCrate);
                pickUpState.StateStart();
                break;
            case AIState.Carrying:
                currentState = AIState.Carrying;
                carryingState = new CS_ArrowSupply_Carrying(characterAI, carriedArrow);
                carryingState.StateStart();
                break;
            case AIState.Delivery:
                currentState = AIState.Delivery;
                // Choose a random delivery location
                currentDeliveryLocation = deliveryLocations[Random.Range(0, deliveryLocations.Length)];
                deliveryState = new CS_ArrowSupply_Delivery(characterAI, null, carriedArrow); // Adjust this line to accept a Transform parameter
                deliveryState.StateStart();
                break;
        }
    }

    // Method to initiate the pickup action
    public void InitiatePickup()
    {
        SetState(AIState.PickUp);
    }

    // Method to initiate the delivery action
    public void InitiateDelivery(ArrowSupply_Arrow arrow)
    {
        carriedArrow = arrow;
        SetState(AIState.Delivery);
    }
}
