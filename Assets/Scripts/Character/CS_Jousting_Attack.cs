using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Attack : CharacterState
{
    private JoustingMatch match;
    private Character other;
    private UI_Jousting ui;
    private CS_Jousting_Riding riding;
    private Jousting_Weapon weapon;

    private bool animationStarted = false;
    private float animationDuration;
    private bool impactState = false; 

    public CS_Jousting_Attack(Character character, Jousting_Weapon weapon) : base(character)
    {
        match = (JoustingMatch)Game.Match;
        other = match.OtherCharacter(character.PlayerIndex);
        this.weapon = weapon;
    }

    public override void StateStart()
    {
        if (character.PlayerIndex == 0 && !animationStarted)
        {
            character.Animator.CrossFade("Attack_Right_Forward_RH", 0.1f);
            animationStarted = true;
            animationDuration = character.Animator.GetCurrentAnimatorStateInfo(0).length;
        }
    }

    public override void Tick()
    {
        if (animationStarted)
        {
            animationDuration -= Time.deltaTime;

            if (animationDuration <= 0)
            {
                if (weapon.hasCollided && !impactState)
                {
                    character.SetNewState(new CS_Jousting_Impact(character));
                    impactState = true; 
                }

                character.SetNewState(new CS_Jousting_Riding(character));
            }
        }
        character.transform.position += character.transform.forward * match.HorseSpeed * Time.deltaTime;
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
        animationStarted = false;
    }
}