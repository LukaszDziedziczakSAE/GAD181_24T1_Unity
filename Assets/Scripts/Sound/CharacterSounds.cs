using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] aSounds;
    [SerializeField] AudioClip[] grunts;

    private void PlayAudioClip(AudioClip clip)
    {
        if (audioSource == null) return;

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayASound()
    {
        if (aSounds.Length == 0) return;

        PlayAudioClip(aSounds[Random.Range(0, aSounds.Length)]);
    }

    public void PlayGruntSound()
    {
        if (grunts.Length == 0) return;

        PlayAudioClip(grunts[Random.Range(0, grunts.Length)]);
    }
}
