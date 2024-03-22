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

    private bool isAnimationPlaying = false; // Flag to indicate if the animation is playing

    public CS_ArrowSupply_PickUp(Character character, ArrowSupply_Crate crate) : base(character)
    {
        this.crate = crate;
    }

    public override void StateStart()
    {
        lastPosition = character.transform.position;

        character.Animator.CrossFade("ScavangerHunt_Pickup", 0.2f);

        character.NavMeshAgent.isStopped = true;

        isAnimationPlaying = true; // Set animation flag to true

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

        // Check if animation is playing
        if (isAnimationPlaying)
        {
            // If animation is playing and finished
            if (character.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Pickup") && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                // Set flag to false and allow movement
                isAnimationPlaying = false;
                EnableMovement();
                character.SetNewState(new CS_ArrowSupply_Carrying(character, arrow));
            }
            else
            {
                // If animation is still playing, disable movement
                DisableMovement();
            }
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

    // Function to disable movement
    private void DisableMovement()
    {
        // Example: Disable movement by setting the NavMeshAgent's destination to its current position
        character.NavMeshAgent.destination = character.transform.position;
    }

    // Function to enable movement
    private void EnableMovement()
    {
        // Example: Enable movement by resetting NavMeshAgent's destination
        character.NavMeshAgent.ResetPath();
    }
}