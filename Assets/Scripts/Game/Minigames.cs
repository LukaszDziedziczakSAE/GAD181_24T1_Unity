using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigames : MonoBehaviour
{
    //[field: SerializeField] public Game[] Games {  get; private set; }
    [field: SerializeField] public MinigameConfig[] Configs { get; private set; }


    /*[System.Serializable]
    public class Game
    {
        public int buildIndex;
        public string Name;
        public string Description;
        public Texture selectionCardPicture;
        public Texture startTitleCardPicture;

        public void Play()
        {
            SceneManager.LoadScene(buildIndex);
        }
    }*/

    public MinigameConfig[] RandomGames
    {
        get
        {
            List<MinigameConfig> temp = Configs.ToList();
            List<MinigameConfig> list = new List<MinigameConfig>();
            while (temp.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, temp.Count);
                list.Add(temp[random]);
                temp.RemoveAt(random);

            }
            return list.ToArray();
        }
    }
}
