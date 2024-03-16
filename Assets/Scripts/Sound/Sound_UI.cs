using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_UI : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonPress;
    [SerializeField] AudioClip buttonPressConfirm;
    [SerializeField] AudioClip buttonPressCancel;
    [SerializeField] AudioClip awardPointSound;

    private void PlayAudioClip(AudioClip clip)
    {
        if (audioSource == null) return;

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayButtonPressSound()
    {
        if (buttonPress != null) PlayAudioClip(buttonPress);
    }

    public void PlayButtonPressConfirmSound()
    {
        if (buttonPress != null) PlayAudioClip(buttonPressConfirm);
    }

    public void PlayButtonPressCancelSound()
    {
        if (buttonPress != null) PlayAudioClip(buttonPressCancel);
    }

    public void PlayAwardPointSound()
    {
        if (awardPointSound != null) PlayAudioClip(awardPointSound);
    }
}
