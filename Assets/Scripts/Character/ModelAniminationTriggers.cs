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
        ((CS_ScavangerPickUp)character.State).Grab();
    }

    void PickUpFinish()
    {
        ((CS_ScavangerPickUp)character.State).GrabComplete();
        //((ScavangerHuntMatch)Game.Match).PlayerPickedUp();
    }
}
