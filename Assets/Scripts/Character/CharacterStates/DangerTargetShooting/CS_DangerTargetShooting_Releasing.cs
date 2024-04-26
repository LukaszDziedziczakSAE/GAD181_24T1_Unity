using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_DangerTargetShooting_Releasing : CharacterState
{
    float drawPower;
    TargetShooting_ArrowLauncher arrowLauncher;
    TargetShooting_AI ai;
    public CS_DangerTargetShooting_Releasing(Character character, float drawPower) : base(character)
    {
        this.drawPower = drawPower;
        arrowLauncher = character.GetComponentInChildren<TargetShooting_ArrowLauncher>();
        ai = character.GetComponentInChildren<TargetShooting_AI>();
    }

    public override void StateStart()
    {
        //character.SetNewState(new CS_Archering_Standing(character));
        character.Animator.CrossFade("TargetShooting_ArrowShoot", 0.1f);

        arrowLauncher.FireArrow(drawPower);
    }

    public override void Tick()
    {
        if (character.Animator.GetCurrentAnimatorStateInfo(0).IsTag("ArrowShoot") && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            //SaveRotation(character.transform.rotation);
            character.SetNewState(new CS_DangerTargetShooting_Standing(character));
        }

    }
    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {
        if (ai != null) ai.ResetTimer();
    }

}
