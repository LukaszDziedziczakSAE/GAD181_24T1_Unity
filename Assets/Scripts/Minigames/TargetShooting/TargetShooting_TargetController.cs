using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_TargetController : MonoBehaviour
{
    /*[SerializeField] TargetShooting_Target[] targets;*/
    [SerializeField] TargetShooting_Target[] easyTargets;
    [SerializeField] TargetShooting_Target[] mediumTargets;
    [SerializeField] TargetShooting_Target[] hardTargets;
    [SerializeField] TargetShooting_Target[] veryHardTargets;
    private bool isReady;

    ArrowShootingMatch match => (ArrowShootingMatch)Game.Match;
    //create 3 lists with all targets in level in these lists and player level determine which list the use
    private void OnEnable()
    {
        TargetShooting_Target.OnTargetPoppedUp += TargetShooting_Target_OnTargetPoppedUp;
        isReady = true;
    }

    private void TargetShooting_Target_OnTargetPoppedUp(TargetShooting_Target target)
    {
        isReady = true;        
    }

    private void OnDisable()
    {
        TargetShooting_Target.OnTargetPoppedUp -= TargetShooting_Target_OnTargetPoppedUp;
    }
    private void Start()
    {
        
    }

    public void LowerAllTargets()
    {
        foreach (TargetShooting_Target target in easyTargets)
        {
            target.SetDownRoation();
        }
        foreach (TargetShooting_Target target in mediumTargets)
        {
            target.SetDownRoation();
        }
        foreach (TargetShooting_Target target in hardTargets)
        {
            target.SetDownRoation();
        }
        foreach (TargetShooting_Target target in veryHardTargets)
        {
            target.SetDownRoation();
        }
    }

    public void RaiseRandomTarget()
    {
        if (!isReady)  return; 

        randomDownTarget.StartRotatingUp();
        isReady = false;
    }

    private TargetShooting_Target randomDownTarget
    {
        get
        {
            TargetShooting_Target target = null;

            while (target == null)
            {
                TargetShooting_Target random = targets[Random.Range(0, targets.Length)];
                if (random.IsDown) target = random;
            }

            return target;
        }
    }


    private TargetShooting_Target[] targets
    {
        get
        {
            switch (match.Difficulty)
            {
                case ArrowShootingMatch.EDifficulty.Easy:
                    return easyTargets;

                case ArrowShootingMatch.EDifficulty.Medium:
                    return mediumTargets;

                case ArrowShootingMatch.EDifficulty.Hard:
                    return hardTargets;

                case ArrowShootingMatch.EDifficulty.VeryHard:
                    return veryHardTargets;

                default:
                    return new TargetShooting_Target[0];
            }
        }
    }
}
