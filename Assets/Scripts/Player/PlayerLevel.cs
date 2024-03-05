using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour, ISaveable
{
    [field: SerializeField] public int Level {  get; private set; }
    [field: SerializeField] public int Experiance { get; private set; }
    [field: SerializeField] public int[] ExperianceRequriments { get; private set; }

    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();

        state.Add("Level", Level);
        state.Add("Experiance", Experiance);

        return state;
    }

    public void RestoreState(object state)
    {
        Dictionary<string, object> restoredState = (Dictionary<string, object>)state;

        Level = (int)restoredState["Level"];
        Experiance = (int)restoredState["Experiance"];
    }

    public void AddExperiance(int amount)
    {
        Experiance += amount;

        while (Experiance >= CurrentRequriment)
        {
            Experiance -= CurrentRequriment;
            Level++;
        }

    }

    public int CurrentRequriment
    {
        get
        {
            return ExperianceRequriments[Level];
        }
    }
}
