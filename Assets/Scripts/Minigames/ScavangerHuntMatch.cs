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
        base.PrematchStart();
        //Result = new MatchResult(Compeditors.Length);
        if (pickUpSpawner != null) await pickUpSpawner.SpawnPickUpsTask();

        Game.CameraManager.SetStartingCamera(preMatchCam);
    }

    protected override void PrematchTick()
    {
        base.PrematchTick();
        /*if (pickUpSpawner != null && pickUpSpawner.SpawnComplete)
        {
            Mode = EState.inProgress;
        }*/
        if (matchTime > -2.5f && !Game.CameraManager.IsCurrentCamera(characterChaseCam))
        {
            Game.CameraManager.SwitchTo(characterChaseCam, 2f);
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
}
