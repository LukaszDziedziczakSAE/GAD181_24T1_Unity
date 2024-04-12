using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jousting_AI : AI
{
    //private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;
    private CS_Jousting_Riding riding;
    private CS_Jousting_Impact impact;
    private Jousting_Weapon weapon;

    public void Attack()
    {
        Debug.Log("Attack method called");

        character.Animator.CrossFade("Jousting_Rider_Attack", 0.1f);
        weapon.SetOwner(character);
        //weapon.ColiderEnabled(true);
        Debug.Log("Attack animation started");

        //if (impact != null && weapon != null && !impact.impactState)
        //{
        //    character.Animator.CrossFade("Jousting_Rider_Attack", 0.1f);
        //    weapon.SetOwner(character);
        //    weapon.ColiderEnabled(true);
        //    Debug.Log("Attack animation started");
        //}
        //else
        //{
        //    Debug.Log("Attack conditions not met");
        //}
    }
}