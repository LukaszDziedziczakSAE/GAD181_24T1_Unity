using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  UI_Main : MonoBehaviour
{
    [field: SerializeField] public UI_MatchTitleCard MatchStart {  get; private set; }
    [field: SerializeField] public UI_MatchStatus MatchStatus { get; private set; }
    [field: SerializeField] public UI_MatchEnd MatchEnd { get; private set; }

    public abstract void LevelLoaded();

    public void UpdateMatchStatus()
    {
        MatchStatus.UpdateStatus();
    }
}
