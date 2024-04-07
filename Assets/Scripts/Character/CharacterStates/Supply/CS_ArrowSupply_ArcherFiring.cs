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
            int firingLineIndex = DetermineFiringLineIndex(character);
            if (firingLineIndex == -1) return null; // Early exit if index not found

            ArrowSupply_FiringLine firingLine = match.FiringLines[firingLineIndex].GetComponent<ArrowSupply_FiringLine>();
            if (firingLine == null) return null; // Check if the FiringLine component is missing

            Character closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (Character enemy in firingLine.enemiesInLine)
            {
                if (enemy == null || enemy.gameObject == null) continue; // Skip destroyed enemies

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

    private int DetermineFiringLineIndex(Character archer)
    {
        // Using the archer's name to match with the firing line index
        switch (archer.gameObject.name)
        {
            case "ArcherOne":
                return 0; // Corresponds to FiringLines[0]
            case "ArcherTwo":
                return 1; // Corresponds to FiringLines[1]
            case "ArcherThree":
                return 2; // Corresponds to FiringLines[2]
            case "ArcherFour":
                return 3; // Corresponds to FiringLines[3]
            default:
                Debug.LogError($"Archer name does not match expected pattern: {archer.gameObject.name}");
                return -1;
        }
    }

    private bool IsEnemyInFiringLine(Character enemy, Transform firingLine)
    {
        // Placeholder: Implement your logic to check if the enemy is in the correct firing line
        // For now, we're assuming all enemies are valid targets. You may want to check their position relative to the firing line.
        return true;
    }
}