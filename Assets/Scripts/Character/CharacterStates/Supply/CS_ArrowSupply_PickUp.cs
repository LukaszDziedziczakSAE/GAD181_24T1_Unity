using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CS_ArrowSupply_PickUp : CharacterState
{
    ArrowSupply_Crate crate;

    ArrowSupply_Arrow arrow;

    private bool isAnimationPlaying = false; // Flag to indicate if the animation is playing

    private ArrowSupply_AIStateHolder stateHolder;

    ArrowSupply_AINavigationController aiController;

    public CS_ArrowSupply_PickUp(Character character, ArrowSupply_Crate crate) : base(character)
    {
        this.crate = crate;
    }

    public override void StateStart()
    {
        if (stateHolder == null)
        {
            stateHolder = GameObject.FindObjectOfType<ArrowSupply_AIStateHolder>();
        }
                
        character.Animator.CrossFade("ScavangerHunt_Pickup", 0.1f);
                
        isAnimationPlaying = true; // Set animation flag to true

        Debug.Log(character.PlayerIndex + " has entered the pickup state");
        
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