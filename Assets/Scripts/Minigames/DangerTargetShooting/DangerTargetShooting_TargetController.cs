using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerTargetShooting_TargetController : MonoBehaviour
{
    /*[SerializeField] TargetShooting_Target[] targets;*/
    [SerializeField] DangerTargetShooting_Target[] easyTargets;
    [SerializeField] DangerTargetShooting_Target[] mediumTargets;
    [SerializeField] DangerTargetShooting_Target[] hardTargets;
    [SerializeField] DangerTargetShooting_Target[] veryHardTargets;
    private bool isReady;

    DangerTargetShooting_Match match => (DangerTargetShooting_Match)Game.Match;
    //create 3 lists with all targets in level in these lists and player level determine which list the use
    private void OnEnable()
    {
        DangerTargetShooting_Target.OnTargetPoppedUp += DangerTargetShooting_Target_OnTargetPoppedUp;
        isReady = true;
    }

    private void DangerTargetShooting_Target_OnTargetPoppedUp(DangerTargetShooting_Target target)
    {
        isReady = true;
    }

    private void OnDisable()
    {
        DangerTargetShooting_Target.OnTargetPoppedUp -= DangerTargetShooting_Target_OnTargetPoppedUp;
    }
    private void Start()
    {

    }

    public void LowerAllTargets()
    {
        foreach (DangerTargetShooting_Target target in easyTargets)
        {
            target.SetDownRoation();
        }
        foreach (DangerTargetShooting_Target target in mediumTargets)
        {
            target.SetDownRoation();
        }
        foreach (DangerTargetShooting_Target target in hardTargets)
        {
            target.SetDownRoation();
        }
        foreach (DangerTargetShooting_Target target in veryHardTargets)
        {
            target.SetDownRoation();
        }
    }

    public void RaiseRandomTarget()
    {
        if (!isReady) return;

        randomDownTarget.StartRotatingUp();
        isReady = false;
    }

    private DangerTargetShooting_Target randomDownTarget
    {
        get
        {
            DangerTargetShooting_Target target = null;

            while (target == null)
            {
                DangerTargetShooting_Target random = targets[Random.Range(0, targets.Length)];
                if (random.IsDown) target = random;
            }

            return target;
        }
    }


    private DangerTargetShooting_Target[] targets
    {
        get
        {
            switch (match.Difficulty)
            {
                case DangerTargetShooting_Match.EDifficulty.Easy:
                    return easyTargets;

                case DangerTargetShooting_Match.EDifficulty.Medium:
                    return mediumTargets;

                case DangerTargetShooting_Match.EDifficulty.Hard:
                    return hardTargets;

                case DangerTargetShooting_Match.EDifficulty.VeryHard:
                    return veryHardTargets;

                default:
                    return new DangerTargetShooting_Target[0];
            }
        }
    }
}
