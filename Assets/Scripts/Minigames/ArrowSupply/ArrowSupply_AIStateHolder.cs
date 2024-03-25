using UnityEngine;

public class ArrowSupply_AIStateHolder : MonoBehaviour
{
    public enum AIState { Locomotion, Carrying, Idle } // Add more states as needed

    
    private AIState currentState = AIState.Idle;// The current state of the AI character

    public void Start()
    {

    }
    
    public void SetState(AIState newState) // Method to set the current state of the AI character
    {
        currentState = newState;
                
        // Debug.Log($"AI State changed to: {currentState}");
    }

    
    public AIState GetState() // Method to get the current state of the AI character
    {
        return currentState;
    }
}
