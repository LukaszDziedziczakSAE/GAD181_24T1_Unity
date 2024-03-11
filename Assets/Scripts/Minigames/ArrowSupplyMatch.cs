using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupplyMatch : MinigameMatch
{
    [SerializeField] float matchLength = 60;
    [field: SerializeField] public float EnemySpeed { get; private set; } = 1;
    [SerializeField] GameObject selectionIndicatorPrefab;

    public float MatchTimeRemaining => matchLength - MatchTime;

    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected override void PrematchStart()
    { 
        base.PrematchStart();
    }

    protected override void PrematchTick()
    {
        base.PrematchTick();

    }

    protected override void MatchStart()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_ArrowSupply_Locomotion(character));
        }

        foreach (Character archer in Archers)
        {
            archer.SetNewState(new CS_ArrowSupply_ArcherWaiting(archer));
        }

        Game.UI.MatchStatus.gameObject.SetActive(true);
    }

    protected override void MatchTick()
    {
        base.MatchTick();

        if (MatchTimeRemaining <= 0)
        {
            Mode = EState.postMatch;
        }
    }

    protected override void PostMatchStart()
    {
        base.PostMatchStart();
        //Mode = EState.none;
    }

    public void ShowTouchIndicator(Vector3 position)
    {
        GameObject selectionIndicator = Instantiate(selectionIndicatorPrefab, position, Quaternion.identity);
    }

    public Character[] Archers
    {
        get
        {
            List<Character> archers = new List<Character>();

            Character[] inLevel = FindObjectsOfType<Character>();
            foreach (Character character in inLevel)
            {
                if (character.PlayerIndex == 101) archers.Add(character);
            }

            return archers.ToArray();
        }
    }

    public Character[] AS_Enemies
    {
        get
        {
            List<Character> archers = new List<Character>();

            Character[] inLevel = FindObjectsOfType<Character>();
            foreach (Character character in inLevel)
            {
                if (character.PlayerIndex == 102) archers.Add(character);
            }

            return archers.ToArray();
        }
    }
}
