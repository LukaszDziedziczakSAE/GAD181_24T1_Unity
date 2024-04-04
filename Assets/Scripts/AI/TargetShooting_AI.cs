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
    [SerializeField] float targetDistanceOffset = 0.1f;
    [SerializeField] Vector2 missPowerOffsetRange;
    [SerializeField] Vector2 missRotationOffsetRange;

    float timer;
    bool isFiring;
    TargetShooting_Target currentTarget;



    private void OnEnable()
    {
        TargetShooting_Target.OnTargetPoppedUp += TargetShooting_Target_OnTargetPoppedUp;
    }
    private void OnDisable()
    {
        TargetShooting_Target.OnTargetPoppedUp -= TargetShooting_Target_OnTargetPoppedUp;
    }

    private void TargetShooting_Target_OnTargetPoppedUp(TargetShooting_Target target)
    {
        currentTarget = target;
    }

    private void Start()
    {
        ResetTimer();
    }



    private void Update()
    {
        if (Game.Match == null) return;
        if (Game.Match.Mode != MinigameMatch.EState.inProgress) return;
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
        if (currentTarget == null) return;        
        float distance = Vector3.Distance(character.transform.position, currentTarget.transform.position)+ targetDistanceOffset;
        float power = distance / 5;

        character.transform.LookAt(currentTarget.transform);

        if (canFire)
        {
            Debug.Log("ai hit");
        }
        else
        {
            float powerOffset = coinFlip ? missPowerOffset : -missPowerOffset;
            power += powerOffset;
            float currentRotation = character.transform.eulerAngles.y;
            float rotationOffset = coinFlip ? missRotationOffset : -missRotationOffset;
            currentRotation += rotationOffset;
            character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x,currentRotation, character.transform.eulerAngles.z);

            Debug.Log("ai miss, poweroffset is " + powerOffset + " , rotationOffset is " + rotationOffset);
            
        }
        character.SetNewState(new CS_Archering_Releasing(character, power));
       
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

    private bool coinFlip
    {
        get
        {
            int random = Random.Range(0,2);
            return random == 1;
        }
    }

    private float missPowerOffset
    {
        get
        {
            return Random.Range(missPowerOffsetRange.x, missPowerOffsetRange.y);
        }
    }

    private float missRotationOffset
    {
        get
        {
            return Random.Range(missRotationOffsetRange.x, missRotationOffsetRange.y);
        }
    }

}
