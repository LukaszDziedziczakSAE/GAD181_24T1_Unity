using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class SeenTutorials : MonoBehaviour, ISaveable
{
    List<TutorialRecord> records = new List<TutorialRecord>();

    [System.Serializable]
    public class TutorialRecord
    {
        public string MinigameName;
        public int TutorialIndex;

        public TutorialRecord(string minigameName, int tutorialIndex)
        {
            MinigameName = minigameName;
            TutorialIndex = tutorialIndex;
        }
    }

    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();

        state.Add("records", records);

        return state;
    }

    public void RestoreState(object state)
    {
        Dictionary<string, object> restoredState = (Dictionary<string, object>)state;

        records = (List<TutorialRecord>)restoredState["records"];
    }

    public void AddTutorialRecord(MinigameConfig config, int index)
    {
        records.Add(new TutorialRecord(config.Name, index));
    }

    public bool HasSeenTutorial(MinigameConfig config, int index)
    {
        foreach (TutorialRecord record in records)
        {
            if (record.MinigameName == config.Name && record.TutorialIndex == index)
            {
                return true;
            }
        }

        return false;
    }

    public void ResetPlayer()
    {
        records.Clear();
    }
}
