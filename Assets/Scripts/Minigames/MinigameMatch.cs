using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MinigameMatch : MonoBehaviour
{
    public abstract void MatchStart();

    public abstract MatchResult DetermineResult();

    protected Character[] Compeditors
    {
        get
        {
            List<Character> result = new List<Character>();
            Character[] inLevel = FindObjectsOfType<Character>();
            foreach (Character character in inLevel)
            {
                if (character.PlayerIndex >= 0) result.Add(character);
            }

            return result.ToArray();
        }
    }
}
