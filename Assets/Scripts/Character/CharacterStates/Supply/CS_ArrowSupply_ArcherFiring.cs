using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_ArrowSupply_ArcherFiring : CharacterState
{
    ArrowSupply_ArcherSupply.ArrowRecord arrowRecord;

    ArrowSupply_ArrowFirePoint firePoint;
    private ArrowSupply_Match match => (ArrowSupply_Match)Game.Match;
        
    private float delayBeforeFiring = 1.0f; // Delay in seconds before firing the arrow

    private float timer = 0.0f; // Timer to track the delay

    private bool hasFired = false; // To ensure the arrow is fired only once

    private Character lastTargetedEnemy;

    public CS_ArrowSupply_ArcherFiring(Character character, ArrowSupply_ArcherSupply.ArrowRecord arrowRecord) : base(character)
    {
        this.arrowRecord = arrowRecord;
        character.Animator.CrossFade("TargetShooting_BowIdle", 0.1f);
    }

    public override void StateStart()
    {
        firePoint = character.GetComponentInChildren<ArrowSupply_ArrowFirePoint>();

        timer = 0.0f;

        hasFired = false;

        lastTargetedEnemy = null; // Initialize lastTargetedEnemy at state start
    }


    public override void Tick()
    {
        if (!hasFired)
        {
            timer += Time.deltaTime;

            if (timer >= delayBeforeFiring)
            {
                Character enemy = closestEnemy;
                if (enemy != null)
                {
                    if (enemy != lastTargetedEnemy)
                    {
                        int lastIndex = DetermineFiringLineIndex(lastTargetedEnemy);
                        if (lastIndex != -1 && match.Popups[lastIndex].GetComponent<ArrowSupply_ArrowPopup>().isActiveAndEnabled)
                        {
                            match.DeactivatePopup(lastIndex);
                        }
                        lastTargetedEnemy = enemy;
                    }

                    firePoint.FireArrow(arrowRecord, enemy);

                    hasFired = true;

                    Debug.Log($"Archer {character.name} has fired an arrow at {enemy.name}");

                    // Call centralized method to potentially activate popup
                    int archerIndex = DetermineFiringLineIndex(character);

                    if (archerIndex != -1)
                    {
                        CharacterModel enemyModel = enemy.GetComponentInChildren<CharacterModel>();

                        if (enemyModel != null && enemyModel.CurrentConfig != null)
                        {
                            Debug.Log($"Triggering ArrowCall for {enemy.name} with variant {enemyModel.CurrentConfig.Variant}");

                            match.ArrowCall(archerIndex, enemyModel.CurrentConfig.Variant);
                        }
                        else
                        {
                            Debug.Log("CharacterModel component not found or CurrentConfig is null on " + enemy.name);
                        }
                    }
                    else
                    {
                        Debug.LogError($"Invalid archer index for {character.name}");
                    }

                    // Change state if needed after firing
                    character.SetNewState(new CS_ArrowSupply_ArcherWaiting(character));
                }
                else
                {
                    Debug.Log("No enemy found to fire at.");
                }
            }
        }
    }


    private void UpdateUIForArcher(Character enemy)
    {
        int archerIndex = DetermineFiringLineIndex(character);

        if (archerIndex != -1 && enemy != null)
        {
            // Get the CharacterModel component from the enemy
            CharacterModel enemyModel = enemy.GetComponent<CharacterModel>();

            if (enemyModel != null && enemyModel.CurrentConfig != null) // Ensure CurrentConfig is not null
            {
                Transform popupTransform = match.Popups[archerIndex];

                ArrowSupply_ArrowPopup popup = popupTransform.GetComponent<ArrowSupply_ArrowPopup>();
                if (popup != null)
                {
                    // Use CurrentConfig to access Variant
                    popup.UpdateIcon(enemyModel.CurrentConfig.Variant);
                }
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

    public int DetermineFiringLineIndex(Character archer)
    {
        if (archer == null || archer.gameObject == null)
        {
            //Debug.LogError("Archer or Archer GameObject is null.");
            return -1;
        }

        switch (archer.gameObject.name)
        {
            case "ArcherOne":
                return 0;
            case "ArcherTwo":
                return 1;
            case "ArcherThree":
                return 2;
            case "ArcherFour":
                return 3;
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