using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAniminationTriggers : MonoBehaviour
{
    Character character;

    private void Awake()
    {
        character = GetComponentInParent<Character>();
    }

    void PickUpStart()
    {
        if (character.State.GetType() == new CS_ScavangerPickUp(character, null).GetType())
        {
            ((CS_ScavangerPickUp)character.State).Grab();
        }

        else if (character.State.GetType() == new CS_ArrowSupply_PickUp(character, null).GetType())
        {
            ((CS_ArrowSupply_PickUp)character.State).Grab();
        }
    }

    void PickUpFinish()
    {
        if (character.State.GetType() != new CS_ScavangerPickUp(character, null).GetType()) return;


        ((CS_ScavangerPickUp)character.State).GrabComplete();
        //((ScavangerHuntMatch)Game.Match).PlayerPickedUp();
    }
}
