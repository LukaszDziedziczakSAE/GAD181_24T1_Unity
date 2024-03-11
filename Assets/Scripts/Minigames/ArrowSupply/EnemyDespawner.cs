using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawner : MonoBehaviour
{
    [SerializeField] int pointsDeducted = 1; 

    ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            foreach (Character character in match.Compeditors)
            {
                match.AwardPlayerPoints(character.PlayerIndex, -pointsDeducted);
            }

            Game.UI.UpdateMatchStatus();
            
            Destroy(other.gameObject);
        }
    }
}
