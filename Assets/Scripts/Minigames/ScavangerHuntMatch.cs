using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerHuntMatch : MinigameMatch
{
    [SerializeField] GameObject selectionIndicatorPrefab;
    public int itemsPickedUp;

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

    public void ShowTouchIndicator(Vector3 position)
    {
        GameObject selectionIndicator = Instantiate(selectionIndicatorPrefab, position, Quaternion.identity);
    }

    public void PlayerPickedUp()
    {
        itemsPickedUp++;
    }
}
