using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerHuntMatch : MinigameMatch
{
    //[SerializeField] float matchLength = 120;
    [SerializeField] int winScore = 10;
    [SerializeField] GameObject selectionIndicatorPrefab;
    [SerializeField] ScavangerHunt_PickUpSpawner pickUpSpawner;
    [SerializeField] CinemachineVirtualCamera characterChaseCam;
    [SerializeField] CinemachineVirtualCamera preMatchCam;
    //public int itemsPickedUp = 0;

    //public float MatchTimeRemaining => matchLength - MatchTime;

    protected async override void PrematchStart()
    {
        //base.PrematchStart();

        if (pickUpSpawner != null)
        {
            await pickUpSpawner.SpawnPickUpsTask();
            //await pickUpSpawner.CheckPositionsTask();
        }
        else
        {
            RegularPreMatchStart();
        }

        Game.CameraManager.SetStartingCamera(preMatchCam);
    }

    protected override void PrematchTick()
    {
        if (!Game.UI.Prematch.gameObject.activeSelf)
        {
            if (pickUpSpawner != null && pickUpSpawner.SpawnComplete)
            {
                RegularPreMatchStart();
            }
        }
        else
        {
            base.PrematchTick();
            if (matchTime > -2.5f && !Game.CameraManager.IsCurrentCamera(characterChaseCam))
            {
                Game.CameraManager.SwitchTo(characterChaseCam, 2f);
            }
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

        /*if (MatchTimeRemaining <= 0)
        {
            Mode = EState.postMatch;
        }*/

        
    }

    protected override void PostMatchStart()
    {
        RemovePickUpFromHands();
        base.PostMatchStart();
    }

    public void ShowTouchIndicator(Vector3 position)
    {
        GameObject selectionIndicator = Instantiate(selectionIndicatorPrefab, position, Quaternion.identity);
    }

    public bool CharacterWon(Character character)
    {
        return Result.Scores[character.PlayerIndex] >= winScore;
    }

    private void RemovePickUpFromHands()
    {
        foreach (Character character in Compeditors)
        {
            ScavangerHunt_PickUp pickUp = character.GetComponentInChildren<ScavangerHunt_PickUp>();
            if (pickUp != null) Destroy(pickUp.gameObject);
        }
    }

    private void RegularPreMatchStart()
    {
        Result = new MatchResult(Compeditors.Length);

        if (Game.UI.Prematch != null)
        {
            Game.UI.Prematch.gameObject.SetActive(true);
            matchTime = -3.5f;
            //print("Prematch time set = " + matchTime);
        }
        else
        {
            Debug.LogWarning("Prematch UI missing from scene");
            Mode = EState.inProgress;
        }
    }
}
