using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigames : MonoBehaviour
{
    [field: SerializeField] public Game[] Games {  get; private set; }

    [System.Serializable]
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
    }
}
