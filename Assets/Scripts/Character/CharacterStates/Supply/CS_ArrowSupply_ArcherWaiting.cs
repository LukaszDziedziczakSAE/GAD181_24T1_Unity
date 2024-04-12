using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_ArcherWaiting : CharacterState
{
    // float fireRate = 1;

    float stateStartTime;

    private ArrowSupply_Match match => (ArrowSupply_Match)Game.Match;

    float timePassed => stateStartTime - Time.time;

    ArrowSupply_ArcherSupply archerSupply;

    public CS_ArrowSupply_ArcherWaiting(Character character) : base(character)
    {
        character.Animator.CrossFade("StandingIdle", 0.1f);
        
    }

    public override void StateStart()
    {
        archerSupply = character.GetComponentInChildren<ArrowSupply_ArcherSupply>();

        stateStartTime = Time.time;

        if (archerSupply == null) Debug.LogError("Missing Archer Supply Referance");
    }

    public override void Tick()
    {
        if (archerSupply.HasArrows)
        {
            character.SetNewState(new CS_ArrowSupply_ArcherFiring(character, archerSupply.TakeArrow()));
        }

        //Debug.Log("Archer has " + archerSupply.Arrows.Count + " arrows");
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

        return true;
    }
}
