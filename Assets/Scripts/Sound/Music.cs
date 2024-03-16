using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] mainMenuTracks;
    [SerializeField] AudioClip[] matchTracks;
    [SerializeField] AudioClip[] victoryTracks;
    [SerializeField] AudioClip[] defeatTracks;
    [SerializeField] AudioClip[] prematchTracks;
    [SerializeField] AudioClip[] postmatchTracks;

    public bool IsPlaying => audioSource.isPlaying;

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

    public void PlayVictoryTrack()
    {
        if (victoryTracks.Length == 0) return;

        PlayAudioClip(victoryTracks[Random.Range(0, victoryTracks.Length)]);
    }

    public void PlayDefeatTrack()
    {
        if (defeatTracks.Length == 0) return;

        PlayAudioClip(defeatTracks[Random.Range(0, defeatTracks.Length)]);
    }

    public void PlayPrematchTracks()
    {
        if (prematchTracks.Length == 0) return;

        PlayAudioClip(prematchTracks[Random.Range(0, prematchTracks.Length)]);
    }

    public void PlayPostMatchTrack()
    {
        if (postmatchTracks.Length == 0) return;

        PlayAudioClip(postmatchTracks[Random.Range(0, postmatchTracks.Length)]);
    }
}
