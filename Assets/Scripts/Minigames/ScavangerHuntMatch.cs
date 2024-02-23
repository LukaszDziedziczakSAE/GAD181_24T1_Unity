using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerHuntMatch : MinigameMatch
{
    [SerializeField] float matchLength = 120;
    [SerializeField] GameObject selectionIndicatorPrefab;
    public int itemsPickedUp;

    public float MatchTimeRemaining => matchLength - MatchTime;

    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected override void MatchStart()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_ScavangerLocomotion(character));
        }
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
        Mode = EState.none;
    }

    public void ShowTouchIndicator(Vector3 position)
    {
        GameObject selectionIndicator = Instantiate(selectionIndicatorPrefab, position, Quaternion.identity);
    }

    public void PlayerPickedUp()
    {
        itemsPickedUp++;
    }
}
