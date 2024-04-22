using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour, ISaveable
{
    [field: SerializeField] public int Level {  get; private set; }
    [field: SerializeField] public int Experiance { get; private set; }
    [field: SerializeField] public int[] ExperianceRequriments { get; private set; }

    [SerializeField] List<string> screensSeen = new List<string>();

    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();

        state.Add("Level", Level);
        state.Add("Experiance", Experiance);
        state.Add("screensSeen", screensSeen);
        return state;
    }

    public void RestoreState(object state)
    {
        Dictionary<string, object> restoredState = (Dictionary<string, object>)state;

        Level = (int)restoredState["Level"];
        Experiance = (int)restoredState["Experiance"];
        if (restoredState.ContainsKey("screensSeen")) screensSeen = (List<string>)restoredState["screensSeen"];

        if (Level == 0) Level = 1;
    }

    public void AddExperiance(int amount)
    {
        Experiance += amount;

        if (Experiance >= CurrentRequriment)
        {
            if (Level < Game.HighestLevel)
            {
                Experiance -= CurrentRequriment;
                Level++;
            }
            else
            {
                Experiance = CurrentRequriment;
            }
            
        }

    }

    public int CurrentRequriment
    {
        get
        {
            if (Level >= ExperianceRequriments.Length) Debug.LogError("Level too high");
            return ExperianceRequriments[Level];
        }
    }

    public void ResetPlayer()
    {
        Level = 1;
        Experiance = 0;
        screensSeen.Clear();
    }

    public void SeenTitleCard(MinigameConfig config)
    {
        screensSeen.Add(config.Name);
    }
    public bool HasSeenTitleCard(MinigameConfig config)
    {
       return screensSeen.Contains(config.Name);
    }
}
