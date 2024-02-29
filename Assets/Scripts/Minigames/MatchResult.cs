using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchResult
{
    public Dictionary<int, int> Scores { get; private set; }

    public MatchResult()
    {
        Debug.LogWarning("New Match Results not created correctly");
    }

    public MatchResult(int amountOfPlayers)
    {
        Scores = new Dictionary<int, int>();
        for (int player = 0; player < amountOfPlayers; player++)
        {
            Scores.Add(player, 0);
        }

        Debug.Log("Created Match Results for " + amountOfPlayers + " players");
    }

    public void AwardPointsToPlayer(int playerNumber, int points)
    {
        if (playerNumber < 0 || playerNumber >= 4) return;

        Scores[playerNumber] += points;
    }
}
