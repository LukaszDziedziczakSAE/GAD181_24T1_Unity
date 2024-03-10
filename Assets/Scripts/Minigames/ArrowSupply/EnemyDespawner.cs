using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawner : MonoBehaviour
{
    [SerializeField] int pointsDeducted = 1; // Change to positive value for deduction

    ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy"))
        {
            foreach (Character character in match.Compeditors)
            {
                match.AwardPlayerPoints(character.PlayerIndex, -pointsDeducted);
            }

            Game.UI.UpdateMatchStatus();
            // Destroy the enemy
            Destroy(other.gameObject);
        }
    }
}
