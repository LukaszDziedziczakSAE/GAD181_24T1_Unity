using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] int pointsAward = 10;
    ScavangerHuntMatch match => (ScavangerHuntMatch)Game.Match;
    PickUpSpawner spawner;
    Character characterInProx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            if (character.State.GetType() != new CS_ScavangerLocomotion(character).GetType()) return;

            characterInProx = character;
            character.SetNewState(new CS_ScavangerPickUp(character, this));
        }
    }

    public void CompletePickUp()
    {
        if (characterInProx == null) return;


        match.AwardPlayerPoints(characterInProx.PlayerIndex, pointsAward);
        Game.UI.UpdateMatchStatus();
        Destroy(this.gameObject);
    }

    public void Spawner(PickUpSpawner pickUpSpawner)
    {
        spawner = pickUpSpawner;
    }

    private void OnDestroy()
    {
        spawner?.RemovePickUp(this);
    }
}
