using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TargetShooting_AI : AI
{
    //variables for finding targets
    //set a rotational range for AIs to shoot arrows and either hit or miss target
    //set a power range for AIs to eith hit os fall short of target
    //set a timer for AI fire rate




    private void Update()
    {
        
    }



    void AimAtTargets()//rotate AIs to face target direction
    {
        //character.transform.eulerAngles = 
    }



    private RaycastHit ActiveTarget(RaycastHit[] hits)
    {
        RaycastHit targetHits = new RaycastHit();
        float activeTargets = Mathf.Infinity;

        foreach (RaycastHit hit in hits)
        {
            float aiming = Vector3.Distance(transform.position, hit.point);

            if (aiming < activeTargets)
            {
                targetHits = hit;
                activeTargets = aiming;
            }
        }
        return targetHits;
    }
}
