using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchResult
{
    public Dictionary<int, int> Scores { get; private set; }
    [field: SerializeField] public Result[] Results { get; private set; }

    public int AmountOfPlayers => Scores.Count;

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

    public void CreateResults()
    { 
        List<int> winOrder = new List<int>();

        while (winOrder.Count != AmountOfPlayers)
        {
            int highestScorePlayerNo = -1;
            int highestScore = 0;

            for (int player = 0; player < AmountOfPlayers; player++)
            {
                if (winOrder.Contains(player)) continue;

                int score = Scores[player];
                if (score > highestScore)
                {
                    highestScore = score;
                    highestScorePlayerNo = player;
                }
            }

            winOrder.Add(highestScorePlayerNo);
        }

        List<Result> results = new List<Result>();
        for (int placement = 1; placement <= AmountOfPlayers; placement++)
        {
            int playerNumber = winOrder[placement - 1];
            if (playerNumber < 0 || playerNumber >= 100) continue;
            Result result = new Result(playerNumber, placement, Scores[playerNumber]);
            results.Add(result);
        }

        Results = results.ToArray();
    }

    [System.Serializable]
    public class Result
    {
        public int PlayerNumber;
        public int Placement;
        public int Score;

        public Result(int playerNumber, int placement, int score)
        {
            PlayerNumber = playerNumber;
            Placement = placement;
            Score = score;
        }

        public int GoldAward => Game.Match.Config.GoldAward(Score, Placement);

        public int XPAward => Game.Match.Config.XPAward(Score, Placement);
    }
}
