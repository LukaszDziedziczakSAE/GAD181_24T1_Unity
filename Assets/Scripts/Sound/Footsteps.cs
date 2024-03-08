using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] footstepSounds;

    private void PlayAudioClip(AudioClip clip)
    {
        if (audioSource == null) return;

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0) return;

        PlayAudioClip(footstepSounds[Random.Range(0, footstepSounds.Length)]);
    }
}
