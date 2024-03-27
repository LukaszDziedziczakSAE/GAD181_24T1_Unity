using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_ArcherFiring : CharacterState
{
    ArrowSupply_ArcherSupply.ArrowRecord arrowRecord;

    ArrowSupply_ArrowFirePoint firePoint;
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;
        
    private float delayBeforeFiring = 1.0f; // Delay in seconds before firing the arrow

    private float timer = 0.0f; // Timer to track the delay

    private bool hasFired = false; // To ensure the arrow is fired only once

    public CS_ArrowSupply_ArcherFiring(Character character, ArrowSupply_ArcherSupply.ArrowRecord arrowRecord) : base(character)
    {
        this.arrowRecord = arrowRecord;
    }

    public override void StateStart()
    {
        firePoint = character.GetComponentInChildren<ArrowSupply_ArrowFirePoint>();

        timer = 0.0f; // Initialize the timer

        hasFired = false; // Reset the firing status
    }

    public override void Tick()
    {
        if (!hasFired)
        {
            timer += Time.deltaTime; // Update the timer

            // Check if the delay duration has passed
            if (timer >= delayBeforeFiring)
            {
                // Fire the arrow and mark as fired
                firePoint.FireArrow(arrowRecord, closestEnemy);

                hasFired = true;

                // Optionally, transition to the next state immediately after firing
                character.SetNewState(new CS_ArrowSupply_ArcherWaiting(character));
            }
        }
    }

    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {

    }

    private Character closestEnemy
    {
        get
        {
            Character closestEnemy = null;

            float closestDistance = Mathf.Infinity;

            foreach (Character enemy in match.AS_Enemies)
            {
                float distance = Vector3.Distance(character.transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestEnemy = enemy;

                    closestDistance = distance;
                }
            }
            return closestEnemy;
        }
    }
}