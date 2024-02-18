using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] string gameFileName = "gameSave";

    public void LoadGameFile()
    {
        Load(gameFileName);
    }

    public void SaveGameFile()
    {
        Save(gameFileName);
    }

    public void DeleteGameFile()
    {
        Delete(gameFileName);
        if (Game.Instance != null && Game.Player != null) Game.Player.ClearPlayer();
    }

    private IEnumerator LoadLastScene(string saveFile)
    {
        Dictionary<string, object> state = LoadFile(saveFile);
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (state.ContainsKey("lastSceneBuildIndex"))
        {
            buildIndex = (int)state["lastSceneBuildIndex"];
        }
        yield return SceneManager.LoadSceneAsync(buildIndex);
        RestoreState(state);
    }

    private void Save(string saveFile)
    {
        Debug.Log("Saving " + saveFile);
        Dictionary<string, object> state = LoadFile(saveFile);
        CaptureState(state);
        SaveFile(saveFile, state);
    }

    private void Load(string saveFile)
    {
        Debug.Log("Loading " + saveFile);
        RestoreState(LoadFile(saveFile));
    }

    private void Delete(string saveFile)
    {
        File.Delete(GetPathFromSaveFile(saveFile));
    }

    private Dictionary<string, object> LoadFile(string saveFile)
    {
        string path = GetPathFromSaveFile(saveFile);
        if (!File.Exists(path))
        {
            return new Dictionary<string, object>();
        }
        using (FileStream stream = File.Open(path, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    private void SaveFile(string saveFile, object state)
    {
        string path = GetPathFromSaveFile(saveFile);
        //print("Saving to " + path);
        using (FileStream stream = File.Open(path, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
        }

        state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
        {
            string id = saveable.GetUniqueIdentifier();
            if (state.ContainsKey(id))
            {
                saveable.RestoreState(state[id]);
            }
        }
    }

    private string GetPathFromSaveFile(string saveFile)
    {
        return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }

    public bool SaveFileExists
    {
        get
        {
            if (File.Exists(GetPathFromSaveFile(gameFileName)))
            {
                Debug.Log("Save file exists");
            }
            else
            {
                Debug.Log("No save file exists");
            }
            
            return File.Exists(GetPathFromSaveFile(gameFileName));
        }
    }
}
