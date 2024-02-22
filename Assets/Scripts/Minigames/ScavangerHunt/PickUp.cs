using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    ScavangerHuntMatch match => (ScavangerHuntMatch)Game.Match;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            character.SetNewState(new CS_ScavangerPickUp(character, this));
        }
    }

    public void CompletePickUp()
    {
        match.PlayerPickedUp();
        Destroy(this.gameObject);
    }
}
