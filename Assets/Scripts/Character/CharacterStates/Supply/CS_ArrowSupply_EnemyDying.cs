using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_EnemyDying : CharacterState
{
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;
    
    public CS_ArrowSupply_EnemyDying(Character character) : base(character)
    {

    }
    public override void StateStart()
    {
        //character.Animator.CrossFade("ScavangerHunt_Pickup", 0.1f);

        DestroyEnemy();


    }

    public override void Tick()
    {
        
    }

    public override void FixedTick()
    {
        
    }

    public override void StateEnd()
    {
        
    }
    public void DestroyEnemy()
    {
        Object.Destroy(character.gameObject);
    }

}
