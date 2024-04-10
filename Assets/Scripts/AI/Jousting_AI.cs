using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jousting_AI : AI
{
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;
    private CS_Jousting_Riding riding;
    private CS_Jousting_Impact impact;
    private Jousting_Weapon weapon;

    public void Awake()
    {
        character = gameObject.GetComponent<Character>();
    }

    public void Attack()
    {
        if (!impact.impactState && !character.Animator.GetCurrentAnimatorStateInfo(0).IsTag("JoustingAttack") && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            character.Animator.CrossFade("Jousting_Rider_Attack", 0.1f);
            weapon.SetOwner(character);
            weapon.ColiderEnabled(true);
        }
    }
}
