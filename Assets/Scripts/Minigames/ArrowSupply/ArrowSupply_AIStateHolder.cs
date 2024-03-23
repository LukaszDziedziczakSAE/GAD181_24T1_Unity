using UnityEngine;

public class ArrowSupply_AIStateHolder : MonoBehaviour
{
    public enum AIState { Locomotion, Carrying, Idle } // Add more states as needed

    public string state;

    // The current state of the AI character
    private AIState currentState = AIState.Idle;

    public void Start()
    {

    }
    // Method to set the current state of the AI character
    public void SetState(AIState newState)
    {
        currentState = newState;

        // Optional: Trigger actions or notifications based on state change here
        Debug.Log($"AI State changed to: {currentState}");
    }

    // Method to get the current state of the AI character
    public AIState GetState()
    {
        return currentState;
    }
}
