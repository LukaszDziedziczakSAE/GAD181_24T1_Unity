using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_TargetController : MonoBehaviour
{
    [SerializeField] TargetShooting_Target[] targets;
    private bool isReady;

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
        foreach (TargetShooting_Target target in targets)
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
}
