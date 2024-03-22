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

    [SerializeField] float baseFireRate;
    [SerializeField] float fireRateDeviation;
    [SerializeField, Range(0,1)] float hitProbability;

    float timer;
    bool isFiring;

    private void Start()
    {
        ResetTimer();
    }

    

    private void Update()
    {
        if (isFiring) return; 
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                TryShootArrow();
            }
        }
        
    }

    public void ResetTimer()
    {
        timer = fireRate;
        isFiring = false;
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

    private void TryShootArrow()
    {
        if (canFire)
        {
            Debug.Log("ai shot");
        }
        else
        {
            Debug.Log("ai miss");
        }
        character.SetNewState(new CS_Archering_Releasing(character, 1));
       
        isFiring = true;
    }

    private float fireRate
    {
        get
        {
            return Random.Range(baseFireRate - fireRateDeviation, baseFireRate + fireRateDeviation);
        }
    }

    private bool canFire
    {
        get
        {
            float random = Random.Range(0f,1f);
            return random < hitProbability;
        }
    }
}
