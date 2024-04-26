using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerTargetShooting_AI : AI
{

    [SerializeField] float easyFireRate;
    [SerializeField] float mediumFireRate;
    [SerializeField] float hardFireRate;
    [SerializeField] float veryHardFireRate;
    [SerializeField] float fireRateDeviation;
    [SerializeField, Range(0, 1)] float hitProbability;
    [SerializeField] float targetDistanceOffset = 0.1f;
    [SerializeField] Vector2 missPowerOffsetRange;
    [SerializeField] Vector2 missRotationOffsetRange;

    float timer;
    bool isFiring;
    TargetShooting_Target currentTarget;
    DangerTargetShooting_Match match => (DangerTargetShooting_Match)Game.Match;


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
        ResetTimer();
    }

    private void Start()
    {
        ResetTimer();
        if (Game.Player.Level.Level > 19)
        {
            hitProbability = 0.5f;
        }
    }



    private void Update()
    {
        if (Game.Match == null) return;
        if (Game.Match.Mode != MinigameMatch.EState.inProgress) return;
        if (isFiring) return;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
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

    private void TryShootArrow()
    {
        if (currentTarget == null) return;
        float distance = Vector3.Distance(character.transform.position, currentTarget.transform.position) + targetDistanceOffset;
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
            character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, currentRotation, character.transform.eulerAngles.z);

            //Debug.Log("ai miss, poweroffset is " + powerOffset + " , rotationOffset is " + rotationOffset);

        }
        character.SetNewState(new CS_Archering_Releasing(character, power));

        isFiring = true;
    }

    private float fireRate
    {
        get
        {
            if (match == null)
            {
                Debug.LogError(character.name + "(AI): missing match referance");
                return 0f;
            }

            switch (match.Difficulty)
            {
                case DangerTargetShooting_Match.EDifficulty.Easy:
                    return Random.Range(easyFireRate - fireRateDeviation, easyFireRate + fireRateDeviation);

                case DangerTargetShooting_Match.EDifficulty.Medium:
                    return Random.Range(mediumFireRate - fireRateDeviation, mediumFireRate + fireRateDeviation);

                case DangerTargetShooting_Match.EDifficulty.Hard:
                    return Random.Range(hardFireRate - fireRateDeviation, hardFireRate + fireRateDeviation);

                case DangerTargetShooting_Match.EDifficulty.VeryHard:
                    return Random.Range(veryHardFireRate - fireRateDeviation, veryHardFireRate + fireRateDeviation);

                default:
                    return Mathf.Infinity;
            }
        }
    }

    private bool canFire
    {
        get
        {
            float random = Random.Range(0f, 1f);
            return random < hitProbability;
        }
    }

    private bool coinFlip
    {
        get
        {
            int random = Random.Range(0, 2);
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
