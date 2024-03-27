using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    //private EnemyType enemyType;

    Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        // Assign the enemy type based on the component attached to the enemy
        //enemyType = GetComponent<EnemyType>();
    }

    // Method to handle taking damage
    public void TakeDamage(int damageAmount/*, ArrowSupply_Arrow.EType projectileType*/)
    {
        /*if (enemyType != null)
        {
            switch (projectileType)
            {
                case ArrowSupply_Arrow.EType.normal:
                    damageAmount *= enemyType.NormalDamageMultiplier;
                    break;
                case ArrowSupply_Arrow.EType.fire:
                    damageAmount *= enemyType.FireDamageMultiplier;
                    break;
                case ArrowSupply_Arrow.EType.ice:
                    damageAmount *= enemyType.IceDamageMultiplier;
                    break;
                default:
                    break;
            }
        }*/

        currentHealth -= damageAmount;
        
        if (currentHealth <= 0 )
        {
            character.SetNewState(new CS_ArrowSupply_EnemyDying(character));
        }
    }
}

/*public interface EnemyType
{
    int NormalDamageMultiplier { get; }
    int FireDamageMultiplier { get; }
    int IceDamageMultiplier { get; }
}*/