using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MainMenu_Wondering : CharacterState
{
    MainMenuMatch match => (MainMenuMatch)Game.Match;

    public CS_MainMenu_Wondering(Character character) : base(character)
    {
    }

    public override void StateStart()
    {
        character.Animator.CrossFade("ScavangerHunt_Locomotion", 0.1f);
    }

    public override void Tick()
    {
        /*if (character.NavMeshAgent.velocity.magnitude == 0)
        {
            Vector3 newPosition = match.PlayersCharacters.NextSpawnPoint;
            if (newPosition != new Vector3())
            {
                character.NavMeshAgent.SetDestination(newPosition);
            }
        }*/

        if (character.NavMeshAgent.velocity.magnitude > 0)
        {
            character.Animator.SetFloat("speed", 1);
        }

        else
        {
            character.Animator.SetFloat("speed", 0);
        }
    }

    public override void FixedTick()
    {
    }

    public override void StateEnd()
    {
    }
}
