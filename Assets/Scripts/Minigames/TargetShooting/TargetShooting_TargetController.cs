using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_TargetController : MonoBehaviour
{
    [SerializeField] TargetShooting_Target[] targets;

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
        randomDownTarget.StartRotatingUp();
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
