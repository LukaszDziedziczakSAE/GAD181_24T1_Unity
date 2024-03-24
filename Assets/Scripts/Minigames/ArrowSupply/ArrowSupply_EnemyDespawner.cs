using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupply_EnemyDespawner : MonoBehaviour
{
    [SerializeField] int pointsDeducted = 1; 

    ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;

    private void OnTriggerEnter(Collider other)
    {
        Character enemy = other.GetComponent<Character>();
        if (enemy == null || enemy.PlayerIndex != 102) return;


        //foreach (Character character in match.Compeditors)
        {
            //match.AwardPlayerPoints(character.PlayerIndex, -pointsDeducted);
        }

        Game.UI.UpdateMatchStatus();

        Destroy(other.gameObject);
    }
}
