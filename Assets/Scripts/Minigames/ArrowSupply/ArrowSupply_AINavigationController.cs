using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArrowSupply_AINavigationController : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject[] pickupLocations;

    public GameObject[] deliveryLocations;

    private ArrowSupply_AIStateHolder stateHolder;

    private GameObject currentTarget; // To keep track of the current target location

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();

        stateHolder = GetComponent<ArrowSupply_AIStateHolder>();
    }

    bool IsCloseEnoughToTarget(Vector3 targetPosition, float threshold = 1.0f)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);

        return distance <= threshold;
    }
    void Update()
    {
        if (stateHolder.GetState() is ArrowSupply_AIStateHolder.AIState.Locomotion)
        {
            // Only call ApplyLocomotionLogic if the agent is not already moving towards a destination
            if (!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance))
            {
                ApplyLocomotionLogic();
            }
        }
        else if (stateHolder.GetState() is ArrowSupply_AIStateHolder.AIState.Carrying)
        {
            // Similarly for ApplyCarryingLogic
            if (!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance))
            {
                ApplyCarryingLogic();
            }
        }
    }

    void ApplyLocomotionLogic()
    {
        if (pickupLocations.Length > 0)
        {
            int index = Random.Range(0, pickupLocations.Length);

            currentTarget = pickupLocations[index]; // Set the current target

            agent.SetDestination(currentTarget.transform.position);
        }
    }

    void ApplyCarryingLogic()
    {
        if (deliveryLocations.Length > 0)
        {
            int index = Random.Range(0, deliveryLocations.Length);

            currentTarget = deliveryLocations[index]; // Set the current target

            agent.SetDestination(currentTarget.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the AI has entered the trigger collider of the current target
        if (currentTarget != null && other.gameObject == currentTarget)
        {
            // Consider delivery completed and change state as needed
            Debug.Log($"Arrived at {currentTarget.name}");
            
            stateHolder.SetState(ArrowSupply_AIStateHolder.AIState.Idle);

            currentTarget = null; // Clear the current target
        }
    }
}
