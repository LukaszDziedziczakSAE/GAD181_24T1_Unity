using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShootingMatch : MinigameMatch
{

    [field: SerializeField] public float MinimumDrawDistanceToFire { get; private set; }
    [field: SerializeField] public float MaximumDrawDistanceToFire { get; private set; }
    [field: SerializeField] public TargetShooting_TargetController TargetController { get; private set; }

    [SerializeField] float matchLength = 45f;

    public float MatchTimeRemaining => matchLength - MatchTime;
   

    protected override void MatchStart()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Archering_Standing(character));
        }

        TargetController.LowerAllTargets();
        TargetController.RaiseRandomTarget();
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
    }

}
