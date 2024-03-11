using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupplyEnemy : MonoBehaviour
{
    private float speed = 1f; // Speed of the enemy movement

    private void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);// Move the enemy forward
    }
}