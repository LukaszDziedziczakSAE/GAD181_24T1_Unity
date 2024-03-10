using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupplyEnemy : MonoBehaviour
{
    private float speed = 1f; // Speed of the enemy movement

    private void Start()
    {
        
    }
    private void Update()
    {
        // Move the enemy forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}