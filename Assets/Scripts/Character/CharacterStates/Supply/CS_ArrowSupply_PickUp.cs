using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_PickUp : CharacterState
{
    ArrowSupply_Crate crate;

    ArrowSupply_Arrow arrow;

    [SerializeField] private float idleDuration = 1.5f;

    private float idleTimer;

    private Vector3 lastPosition;

    public CS_ArrowSupply_PickUp(Character character, ArrowSupply_Crate crate) : base(character)
    {
        this.crate = crate;
    }

    public override void StateStart()
    {
        lastPosition = character.transform.position;
        character.Animator.CrossFade("ScavangerHunt_Pickup", 0.1f);
        character.NavMeshAgent.isStopped = true;
        Debug.Log(character.PlayerIndex + " has entered the pickup state");
    }

    public override void Tick()
    {
        if (!IsPlayerCharacter && character.transform.position == lastPosition && arrow == null)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleDuration)
            {
                character.SetNewState(new CS_ArrowSupply_Locomotion(character));

                idleTimer = 0f;
            }
            return;
        }
        if (character.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Pickup") && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            character.SetNewState(new CS_ArrowSupply_Carrying(character, arrow));
        }
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
    }

    public void Grab()
    {
        arrow = crate.SpawnInCharactersHand(character);
    }
}
