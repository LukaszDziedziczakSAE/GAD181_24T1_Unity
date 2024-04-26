using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CS_DangerTargetShooting_Standing : CharacterState
{
    public CS_DangerTargetShooting_Standing(Character character) : base(character)
    {
    }

    public override void StateStart()
    {

        //Vector3 targetResetRotation = Vector3.zero;
        //float resetForwardRotation = 
        Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
        character.Animator.CrossFade("TargetShooting_BowIdle", 0.1f);
        //character.transform.eulerAngles = new UnityEngine.Vector3(character.transform.eulerAngles.x, 0, character.transform.eulerAngles.z)* Mathf.(character.transform.eulerAngles, , resetRotationTime);
        //character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.Euler(targetResetRotation), resetRotationTime * Time.deltaTime);

    }

    public override void Tick()
    {

    }
    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {
        Game.InputReader.OnTouchPressed -= InputReader_OnTouchPressed;
    }


    private void InputReader_OnTouchPressed()
    {
        if (character.PlayerIndex != 0)
        {
            return;
        }
        character.SetNewState(new CS_DangerTargetShooting_Drawing(character));
    }
}
