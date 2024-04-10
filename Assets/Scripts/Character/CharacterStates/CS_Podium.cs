using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Podium : CharacterState
{
    bool winner;

    public CS_Podium(Character character, bool winner) : base(character)
    {
        this.winner = winner;
    }

    public override void StateStart()
    {
        if (winner) character.Animator.CrossFade("Podium_Cheering", 0.1f);
        else character.Animator.CrossFade("Podium_Clapping", 0.1f);

        character.NavMeshAgent.enabled = false;
    }

    public override void Tick()
    {
        if (character.Model.transform.localPosition != Vector3.zero)
        {
            character.Model.transform.localPosition = Vector3.zero;
        }

        if (character.Model.transform.localEulerAngles != Vector3.zero)
        {
            character.Model.transform.localEulerAngles = Vector3.zero;
        }
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
        character.NavMeshAgent.enabled = true;
    }
}
