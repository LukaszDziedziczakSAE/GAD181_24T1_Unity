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
            characterInProx = character;
            character.SetNewState(new CS_ScavangerPickUp(character, this));
        }
    }

    public void CompletePickUp()
    {
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
