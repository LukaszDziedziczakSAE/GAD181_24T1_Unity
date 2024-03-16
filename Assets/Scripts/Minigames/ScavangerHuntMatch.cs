using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerHuntMatch : MinigameMatch
{
    [SerializeField] float matchLength = 120;
    [SerializeField] GameObject selectionIndicatorPrefab;
    [SerializeField] ScavangerHunt_PickUpSpawner pickUpSpawner;
    public int itemsPickedUp = 0;

    public float MatchTimeRemaining => matchLength - MatchTime;

    protected async override void PrematchStart()
    {
        Result = new MatchResult(Compeditors.Length);
        if (pickUpSpawner != null) await pickUpSpawner.SpawnPickUpsTask();
        //base.PrematchStart();
    }

    protected override void PrematchTick()
    {
        base.PrematchTick();

        /*if (pickUpSpawner == null || pickUpSpawner.SpawnComplete)
        {
            Mode = EState.inProgress;
        }*/

        if (pickUpSpawner != null && pickUpSpawner.SpawnComplete)
        {
            Mode = EState.inProgress;
        }
        else if (pickUpSpawner == null)
        {
            Mode = EState.inProgress;
        }
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
        //Mode = EState.none;
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
