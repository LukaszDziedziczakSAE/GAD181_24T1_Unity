using MalbersAnimations.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_EnemyLocomotion : CharacterState
{
    EnemyHealth enemyHealth;
    Vector3 startingPosition;

    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;

    public CS_ArrowSupply_EnemyLocomotion(Character character) : base(character)
    {
        enemyHealth = character.GetComponent<EnemyHealth>();
    }

    public override void StateStart()
    {
        character.Animator.CrossFade("ScavangerHunt_Locomotion", 0.1f);
        character.Animator.SetFloat("speed", 1);
        startingPosition = character.transform.position;

        /*if (enemyHealth == null)
        {
            enemyHealth = character.GetComponent<EnemyHealth>();
            if (enemyHealth == null)
            {
                Debug.LogError("EnemyHealth component not found on the character object");
                return;
            }
        }*/
    }

    public override void Tick()
    {
        /*if (character == null)
        {
            Debug.LogError("Character object is null in CS_ArrowSupply_EnemyLocomotion.Tick()");
            return;
        }*/

        /*if (enemyHealth == null)
        {
            enemyHealth = character.GetComponent<EnemyHealth>();
            if (enemyHealth == null)
            {
                Debug.LogError("EnemyHealth component not found on the character object in CS_ArrowSupply_EnemyLocomotion.Tick()");
                return;
            }
        }*/

        character.transform.position += (character.transform.forward * match.EnemySpeed * Time.deltaTime);
        character.transform.position = new Vector3(startingPosition.x, character.transform.position.y, character.transform.position.z);

        /*if (enemyHealth != null && enemyHealth.currentHealth <= 0)
        {
            character.SetNewState(new CS_ArrowSupply_EnemyDying(character));
        }*/
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
        character.Animator.SetFloat("speed", 0);
    }
}