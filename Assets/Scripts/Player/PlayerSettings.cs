using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSettings : MonoBehaviour, ISaveable
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] int qualityLevel;

    public int GraphicQualityLevel => qualityLevel;

    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();

        if (audioMixer.GetFloat("MusicVolume", out float musicVolume))
        {
            state.Add("MusicVolume", musicVolume);
        }

        if (audioMixer.GetFloat("MatchVolume", out float matchVolume))
        {
            state.Add("MatchVolume", matchVolume);
        }

        if (audioMixer.GetFloat("UIVolume", out float uiVolume))
        {
            state.Add("UIVolume", uiVolume);
        }

        state.Add("qualityLevel", qualityLevel);

        return state;
    }

    public void RestoreState(object state)
    {
        Dictionary<string, object> restoredState = (Dictionary<string, object>)state;

        audioMixer.SetFloat("MusicVolume", ( (float)restoredState["MusicVolume"] ));
        audioMixer.SetFloat("MatchVolume", ((float)restoredState["MatchVolume"]));
        audioMixer.SetFloat("UIVolume", ((float)restoredState["UIVolume"]));
        
        if (restoredState.ContainsKey("qualityLevel"))
        {
            qualityLevel = (int)restoredState["qualityLevel"];
            SetQualityLevel(qualityLevel);
        }
        else qualityLevel = QualitySettings.GetQualityLevel();
    }

    public void SetQualityLevel(int level)
    {
        qualityLevel = level;
        QualitySettings.SetQualityLevel(qualityLevel);
    }
}
