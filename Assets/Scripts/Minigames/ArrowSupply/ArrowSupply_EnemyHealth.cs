using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArrowSupply_Arrow;
using static UnityEditor.Rendering.CameraUI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 4;
    private int currentHealth;
    private CharacterModel.Config config;

    private void Start()
    {
        currentHealth = maxHealth;
    }
        public void SetConfig(CharacterModel.Config newConfig)
    {
        config = newConfig;
    }

    // Method to handle taking damage
    public void TakeDamage(int damageAmount, ArrowSupply_Arrow.EType projectileType)
    {
        // Adjust damage based on projectile type
        switch (projectileType)
        {
            case EType.normal:
                
                if (gameObject.CompareTag("Dungeon_Goblin")) 
                { 
                    damageAmount *= 4; 
                }
                
                if (gameObject.CompareTag("Dungeon_Skeleton"))
                {
                    damageAmount *= 1;
                }

                if (gameObject.CompareTag("Dungeon_RockGolem"))
                {
                    damageAmount *= 2;
                }
                break;

            case EType.fire:

                if (gameObject.CompareTag("Dungeon_Goblin"))
                {
                    damageAmount *= 2;
                }

                if (gameObject.CompareTag("Dungeon_Skeleton"))
                {
                    damageAmount *= 4;
                }

                if (gameObject.CompareTag("Dungeon_RockGolem"))
                {
                    damageAmount *= 1;
                }
                break;
            case EType.ice:

                if (gameObject.CompareTag("Dungeon_Goblin"))
                {
                    damageAmount *= 1;
                }

                if (gameObject.CompareTag("Dungeon_Skeleton"))
                {
                    damageAmount *= 2;
                }

                if (gameObject.CompareTag("Dungeon_RockGolem"))
                {
                    damageAmount *= 4;
                }
                break;
            default:
                break;
        }
                
        currentHealth -= damageAmount;

        
        //if (currentHealth <= 0)
        {
            // Perform destruction or any other necessary actions
            //Destroy(gameObject);
        }
    }
}