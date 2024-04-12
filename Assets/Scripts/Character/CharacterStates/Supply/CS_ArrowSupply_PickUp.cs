using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CS_ArrowSupply_PickUp : CharacterState
{
    ArrowSupply_Crate crate;

    ArrowSupply_Arrow arrow;

    private bool isAnimationPlaying = false; // Flag to indicate if the animation is playing

    ArrowSupply_AI aiController;

    private ArrowSupply_AI ai;

    public CS_ArrowSupply_PickUp(Character character, ArrowSupply_Crate crate) : base(character)
    {
        this.crate = crate;
        ai = character.GetComponentInChildren<ArrowSupply_AI>();
    }

    public override void StateStart()
    {
        character.Animator.CrossFade("ScavangerHunt_Pickup", 0.1f);
                
        isAnimationPlaying = true; // Set animation flag to true

        //Debug.Log(character.PlayerIndex + " has entered the pickup state");        
    }

    public override void Tick()
    { 
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
        isAnimationPlaying = false;
    }

    public void Grab()
    {
        arrow = crate.SpawnInCharactersHand(character);        
    }

    // Function to disable movement
    private void DisableMovement()
    {
        character.NavMeshAgent.destination = character.transform.position;
    }

    // Function to enable movement
    private void EnableMovement()
    {
        character.NavMeshAgent.ResetPath();
    }
}