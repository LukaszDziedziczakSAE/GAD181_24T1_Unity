using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Attack : CharacterState
{
    private JoustingMatch match;
    private UI_Jousting ui;
    private Jousting_Weapon weapon;

    public CS_Jousting_Attack(Character character) : base(character)
    {
        match = (JoustingMatch)Game.Match;
        weapon = character.GetComponentInChildren<Jousting_Weapon>();
    }

    public override void StateStart()
    {
        character.Animator.CrossFade("Jousting_Rider_Attack", 0.1f);
        weapon.SetOwner(character);
        weapon.ColiderEnabled(true);
    }

    public override void Tick()
    {
        character.transform.position += character.transform.forward * match.horseSpeed * Time.deltaTime;

        if (character.Animator.GetCurrentAnimatorStateInfo(0).IsTag("JoustingAttack") && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            character.SetNewState(new CS_Jousting_Riding(character));
        }
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
        weapon.ColiderEnabled(false);
    }
}