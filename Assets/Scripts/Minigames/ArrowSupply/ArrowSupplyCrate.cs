using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupplyCrate : MonoBehaviour
{
    public GameObject arrowPrefab; // Reference to the arrow prefab

    private bool isArrowCollected = false; // Flag to track if an arrow has been collected from this crate

    private void OnTriggerEnter(Collider other)
    {
        if (!isArrowCollected && other.CompareTag("Player"))
        {
            CollectArrow(other.gameObject);
        }
    }

    private void CollectArrow(GameObject player)
    {
        
        GameObject arrow = Instantiate(arrowPrefab, player.transform.position, Quaternion.identity);// Instantiate arrow at player's position and rotation

        arrow.transform.parent = transform;

        isArrowCollected = true; // Mark the arrow as collected

    }
}
