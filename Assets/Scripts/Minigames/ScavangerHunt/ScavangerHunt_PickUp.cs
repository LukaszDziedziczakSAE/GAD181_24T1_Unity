using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerHunt_PickUp : MonoBehaviour
{
    [SerializeField] int pointsAward = 1;
    [SerializeField] AudioSource audioSource;
    [SerializeField] ParticleSystem glow;
    ScavangerHuntMatch match => (ScavangerHuntMatch)Game.Match;
    ScavangerHunt_PickUpSpawner spawner;
    Character characterInProx;

    public int Award => pointsAward;

    private void OnTriggerEnter(Collider other)
    {
        if (characterInProx != null) return;
        if (other.TryGetComponent<Character>(out Character character))
        {
            if (character.State.GetType() != new CS_ScavangerLocomotion(character).GetType()) return;

            characterInProx = character;
            character.SetNewState(new CS_ScavangerPickUp(character, this));
            glow.gameObject.SetActive(false);
        }
    }

    public void CompletePickUp()
    {
        if (characterInProx == null) return;
        Destroy(this.gameObject);
    }

    public void Spawner(ScavangerHunt_PickUpSpawner pickUpSpawner)
    {
        spawner = pickUpSpawner;
    }

    private void OnDestroy()
    {
        spawner?.RemovePickUp(this);
    }

    public void PlayPickUpSound()
    {
        if (audioSource == null || audioSource.clip == null) return;
        audioSource.Play();
    }
}
