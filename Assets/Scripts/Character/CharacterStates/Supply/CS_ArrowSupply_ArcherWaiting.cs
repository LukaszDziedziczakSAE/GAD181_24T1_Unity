using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_ArcherWaiting : CharacterState
{
    float stateStartTime;

    private ArrowSupply_Match match => (ArrowSupply_Match)Game.Match;

    float timePassed => Time.time - stateStartTime;

    ArrowSupply_ArcherSupply archerSupply;

    private Character lastTargetedEnemy;

    private const float popupChance = 0.2f; // 20% chance to show popup
    private const float minPopupInterval = 10f; // Minimum 10 seconds between popups
    private float lastPopupTime; // Track the last time a popup was shown

    public CS_ArrowSupply_ArcherWaiting(Character character) : base(character)
    {
        character.Animator.CrossFade("StandingIdle", 0.1f);
    }

    public override void StateStart()
    {
        archerSupply = character.GetComponentInChildren<ArrowSupply_ArcherSupply>();
        stateStartTime = Time.time;
        lastPopupTime = -minPopupInterval; // Ensure a popup can appear initially

        if (archerSupply == null)
            Debug.LogError("Missing Archer Supply Reference");

        lastTargetedEnemy = closestEnemy; // Initialize with the current closest enemy

        if (lastTargetedEnemy != null)
        {
            TryUpdateUIForArcher(lastTargetedEnemy); // Conditionally update the popup
        }
    }

    public override void Tick()
    {
        if (archerSupply.HasArrows)
        {
            Character enemy = closestEnemy;

            if (enemy != lastTargetedEnemy)
            {
                DeactivatePreviousPopup(); // Deactivate previous popup if the target has changed
                lastTargetedEnemy = enemy; // Update the last targeted enemy
                TryUpdateUIForArcher(lastTargetedEnemy); // Conditionally update the popup
            }

            // Pass the current target (lastTargetedEnemy) to the firing state
            character.SetNewState(new CS_ArrowSupply_ArcherFiring(character, archerSupply.TakeArrow(), lastTargetedEnemy));
        }
    }

    private void DeactivatePreviousPopup()
    {
        int lastIndex = DetermineFiringLineIndex(character);
        if (lastIndex != -1 && match.Popups[lastIndex].GetComponent<ArrowSupply_ArrowPopup>().isActiveAndEnabled)
        {
            match.DeactivatePopup(lastIndex);
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

            if (firingLineIndex == -1)
                return null; // Early exit if index not found

            ArrowSupply_FiringLine firingLine = match.FiringLines[firingLineIndex].GetComponent<ArrowSupply_FiringLine>();

            if (firingLine == null)
                return null; // Check if the FiringLine component is missing

            Character closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (Character enemy in firingLine.enemiesInLine)
            {
                if (enemy == null || enemy.gameObject == null)
                    continue; // Skip destroyed enemies

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

    private void TryUpdateUIForArcher(Character enemy)
    {
        if (Time.time - lastPopupTime < minPopupInterval || Random.value > popupChance)
        {
            // Don't show the popup if it's too soon or if the random chance doesn't pass
            return;
        }

        int archerIndex = DetermineFiringLineIndex(character);

        if (archerIndex != -1 && enemy != null)
        {
            CharacterModel enemyModel = enemy.GetComponentInChildren<CharacterModel>();

            if (enemyModel != null && enemyModel.CurrentConfig != null)
            {
                Transform popupTransform = match.Popups[archerIndex];

                ArrowSupply_ArrowPopup popup = popupTransform.GetComponent<ArrowSupply_ArrowPopup>();
                if (popup != null)
                {
                    popup.UpdateIcon(enemyModel.CurrentConfig.Variant);
                    lastPopupTime = Time.time; // Record the time of the last popup
                }
            }
        }
    }
}
