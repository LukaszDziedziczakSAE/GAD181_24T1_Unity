using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] mainMenuTracks;
    [SerializeField] AudioClip[] matchTracks;

    private void PlayAudioClip(AudioClip clip)
    {
        if (audioSource == null) return;

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayMainMenuTrack()
    {
        if (mainMenuTracks.Length == 0) return;

        PlayAudioClip(mainMenuTracks[Random.Range(0, mainMenuTracks.Length)]);
    }

    public void PlayMatchTrack()
    {
        if (matchTracks.Length == 0) return;

        PlayAudioClip(matchTracks[Random.Range(0, matchTracks.Length)]);
    }
}
