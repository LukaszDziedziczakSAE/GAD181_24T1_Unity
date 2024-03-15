using MalbersAnimations.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_EnemyLocomotion : CharacterState
{
    ArrowSupplyMatch match;

    public CS_ArrowSupply_EnemyLocomotion(Character character) : base(character)
    {
        match = (ArrowSupplyMatch)Game.Match;
    }

    public override void StateStart()
    {
        character.Animator.CrossFade("ScavangerHunt_Locomotion", 0.1f);
        character.Animator.SetFloat("speed", 1);
    }

    public override void Tick()
    {
        character.transform.Translate(Vector3.forward * match.EnemySpeed * Time.deltaTime);// Move the enemy forward

        //if (character.Health <= 0)
        {           
           // character.SetNewState(new CS_ArrowSupply_EnemyDying(character));
        }
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
        character.Animator.SetFloat("speed", 0);
    }
}
