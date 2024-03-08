using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupplyMatch : MinigameMatch
{
    [SerializeField] float matchLength = 60;

    [SerializeField] GameObject selectionIndicatorPrefab;

    public float MatchTimeRemaining => matchLength - MatchTime;

    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected async override void PrematchStart()
    { 
        base.PrematchStart();
    }

    protected override void PrematchTick()
    {
        base.PrematchTick();

        /*if (pickUpSpawner == null || pickUpSpawner.SpawnComplete)
        {
            Mode = EState.inProgress;
        }*/
    }

    protected override void MatchStart()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_ArrowSupply_States(character));
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
}
