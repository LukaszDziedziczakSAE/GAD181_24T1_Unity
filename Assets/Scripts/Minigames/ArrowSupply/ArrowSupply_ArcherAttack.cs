using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowSupply_ArcherAttack : MonoBehaviour
{
    public GameObject[] arrowPrefabs; // Array of arrow prefabs representing different arrow types

    public Transform firePoint; // Reference to the point from where arrows are fired

    public LayerMask enemyLayer; // Layer mask for enemies

    public float attackRange = 10f; // Range within which enemies can be attacked

    public float fireRate = 1f; // Rate of fire in arrows per second

    private int currentArrowIndex = 0; // Index of the currently selected arrow type

    private float nextFireTime; // Time of the next arrow fire

    private void Update()
    {
        // Check if it's time to fire an arrow
        if (Time.time >= nextFireTime)
        {
            // Find the closest enemy in front of the archer
            GameObject closestEnemy = FindClosestEnemy();

            // If an enemy is found, fire an arrow at it using the current arrow type
            if (closestEnemy != null)
            {
                FireArrow(closestEnemy.transform.position);
                nextFireTime = Time.time + 1f / fireRate; // Update the next fire time
            }
        }
    }

    private GameObject FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        GameObject closestEnemy = null;

        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in hitColliders)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);

            // Check if the enemy is in front of the archer
            Vector3 directionToEnemy = (collider.transform.position - transform.position).normalized;

            float angle = Vector3.Angle(transform.forward, directionToEnemy);

            if (angle < 90f && distanceToEnemy < closestDistance)
            {
                closestEnemy = collider.gameObject;

                closestDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
    }

    private void FireArrow(Vector3 targetPosition)
    {
        // Instantiate arrow at fire point position and rotation
        GameObject arrow = Instantiate(arrowPrefabs[currentArrowIndex], firePoint.position, Quaternion.identity);

        // Rotate arrow to face the target
        arrow.transform.LookAt(targetPosition);

        // Add force to shoot the arrow
        Rigidbody arrowRigidbody = arrow.GetComponent<Rigidbody>();

        arrowRigidbody.velocity = arrow.transform.forward * 20f; // Adjust this value as needed for arrow speed

        // Cycle to the next arrow type
        currentArrowIndex = (currentArrowIndex + 1) % arrowPrefabs.Length;
    }
}
