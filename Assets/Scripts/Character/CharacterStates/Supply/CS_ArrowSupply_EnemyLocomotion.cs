using MalbersAnimations.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_EnemyLocomotion : CharacterState
{
    EnemyHealth enemyHealth;

    Vector3 startingPosition;

    private ArrowSupply_Match match => (ArrowSupply_Match)Game.Match;

    public CS_ArrowSupply_EnemyLocomotion(Character character) : base(character)
    {
        enemyHealth = character.GetComponent<EnemyHealth>();
    }

    public override void StateStart()
    {
        character.Animator.CrossFade("ScavangerHunt_Locomotion", 0.1f);

        character.Animator.SetFloat("speed", 1);

        startingPosition = character.transform.position;
    }

    public override void Tick()
    {
        character.transform.position += (character.transform.forward * match.EnemySpeed * Time.deltaTime);

        character.transform.position = new Vector3(startingPosition.x, character.transform.position.y, character.transform.position.z);
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
        character.Animator.SetFloat("speed", 0);
    }
}