using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  UI_Main : MonoBehaviour
{
    [field: SerializeField] public UI_MatchTitleCard MatchStart {  get; private set; }
    [field: SerializeField] public UI_Prematch Prematch { get; private set; }
    [field: SerializeField] public UI_MatchStatus MatchStatus { get; private set; }
    [field: SerializeField] public UI_MatchEnd MatchEnd { get; private set; }
    [field: SerializeField] public UI_SettingsMenu SettingsMenu { get; private set; }
    [field: SerializeField] public UI_PauseMenu PauseMenu { get; private set; }

    public abstract void LevelLoaded();

    public void UpdateMatchStatus()
    {
        if (MatchStatus != null)
        {
            MatchStatus.UpdateStatus();
        }
    }
}
