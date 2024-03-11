using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Archering_Releasing : CharacterState
{
    float drawPower;
    TargetShooting_ArrowLauncher arrowLauncher;

    public CS_Archering_Releasing(Character character, float drawPower) : base(character)
    {
        this.drawPower = drawPower;
        arrowLauncher = character.GetComponent<TargetShooting_ArrowLauncher>();
    }

    public override void StateStart()
    {
        character.SetNewState(new CS_Archering_Standing(character));
        //character.Animator.CrossFade("TargetShooting_BowIdle", 0.1f);

        arrowLauncher.FireArrow();
    }

    public override void Tick()
    {
        character.SetNewState(new CS_Archering_Standing(character));
    }
    public override void FixedTick()
    {
        
    }

    public override void StateEnd()
    {
        
    }

    /*void ShootArrow(float releasePower)
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GameObject clone = Instantiate(arrowPrefab, arrowFirePoint.position, Quaternion.identity);

            clone.GetComponent<Rigidbody>().velocity = arrowFirePoint.forward * releasePower * 50;
        }
    }*/
}
