using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_ShootArrow : ArrowShootingMatch
{

    [SerializeField] protected GameObject arrowPrefab;
    [SerializeField] protected Transform arrowFirePoint;
    float startingYPostition;
    private ArrowShootingMatch match;

    private void Start()
    {
        match = GetComponent<ArrowShootingMatch>();
    }

    private void Update()
    {
        CalculatePower();
    }
    void ShootArrow(float releasePower)
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        { 
            GameObject clone = Instantiate(arrowPrefab, arrowFirePoint.position, Quaternion.identity);

            clone.GetComponent<Rigidbody>().velocity = arrowFirePoint.forward * releasePower*50;
        }
    }

    void CalculatePower()
    {
        float currentYPosition = Game.InputReader.TouchPosition.y;
        float distance = startingYPostition - currentYPosition;
        if (distance < 0)
        {
            distance = 0;
        }
        
        float distanceNormalized = distance / match.MaximumDrawDistanceToFire;
        distanceNormalized = Mathf.Clamp(distanceNormalized, 0, 1);

        ShootArrow(distance);
    }
}
