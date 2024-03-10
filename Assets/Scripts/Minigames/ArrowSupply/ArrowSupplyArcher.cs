using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupplyArcher : MonoBehaviour
{
    public GameObject bow; // Reference to the bow GameObject
    [field: SerializeField] public Transform RightHand { get; private set; }
    [field: SerializeField] public Transform LeftHand { get; private set; }

    private void Start()
    {
        // Make sure the bow GameObject is assigned
        if (bow != null)
        {
            // Attach the bow to the NPC's hand
            AttachBowToHand();
        }
        else
        {
            Debug.LogError("Bow GameObject is not assigned!");
        }
    }

    private void AttachBowToHand()
    {
        // Assuming the bow should be attached to a specific hand GameObject
        Transform hand = FindHandTransform(); // You need to implement this method

        if (hand != null)
        {
            // Set the bow's parent to the hand
            bow.transform.parent = hand;
            // Adjust position and rotation as needed
            bow.transform.localPosition = Vector3.zero;
            bow.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError("Hand transform not found!");
        }
    }

    private Transform FindHandTransform()
    {
        // You need to implement logic to find the hand transform
        // This could involve searching for a child GameObject named "Hand" or something similar
        // Alternatively, if your NPC has a rigged model, you might need to find a specific bone representing the hand
        // Return the transform representing the hand
        // If not found, return null
        return null; // Placeholder
    }
}
