using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siege_Launcher : MonoBehaviour
{
    [SerializeField] Transform arm;
    [SerializeField] float readyPosition;
    [SerializeField] float launchedPosition;
    [SerializeField] float launchSpeed;
    [SerializeField] float resetSpeed;
    [SerializeField] Vector3 projectileLocalPostion;
    [SerializeField] Siege_Projectile projectilePrefab;
    [SerializeField] float forwardForce;
    [SerializeField] float upForce;

    Siege_Projectile projectile;
    float timer;
    float lastPos;

    [field: SerializeField] public EState State { get; private set; } = EState.PostLaunch;

    private void OnEnable()
    {
        Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
    }

    private void OnDisable()
    {
        Game.InputReader.OnTouchPressed -= InputReader_OnTouchPressed;
    }

    public enum EState
    {
        Ready,
        Launching,
        PostLaunch,
        Resetting
    }

    private void Update()
    {
        //print(currentArmAngle);
        timer += Time.deltaTime;
        switch (State)
        {
            case EState.Ready: 
                ReadyUpdate();
                break;

            case EState.Launching:
                LaunchingUpdate();
                break;

            case EState.PostLaunch:
                PostLaunchUpdate();
                break;

            case EState.Resetting:
                ResettingUpdate();
                break;
        }
    }

    private void BeginResetting()
    {
        if (State != EState.PostLaunch) return;
        timer = 0;
        lastPos = currentArmAngle;
        State = EState.Resetting;
    }

    private void BeginLaunching()
    {
        if (State != EState.Ready) return;
        timer = 0;
        lastPos = currentArmAngle;
        State = EState.Launching;
    }

    private void ResettingUpdate()
    {
        if (timer >= resetSpeed) SetReady();

        float angle = Mathf.LerpAngle(lastPos, readyPosition, timer / resetSpeed);
        SetArmAngle(angle);
    }

    private void PostLaunchUpdate()
    {

    }

    private void LaunchingUpdate()
    {
        if (timer >= launchSpeed) SetLaunch();

        float angle = Mathf.LerpAngle(lastPos, launchedPosition, timer / launchSpeed);
        SetArmAngle(angle);
    }

    private void ReadyUpdate()
    {

    }

    private void SetArmAngle(float angle)
    {
        arm.localEulerAngles = new Vector3(0, 0, angle);
    }

    private float currentArmAngle => arm.localEulerAngles.z;

    private void SetReady()
    {
        SetArmAngle(readyPosition);
        State = EState.Ready;

        if (projectilePrefab != null)
        {
            projectile = Instantiate(projectilePrefab, arm);
            projectile.transform.localPosition = projectileLocalPostion;
        }
    }

    private void SetLaunch()
    {
        SetArmAngle(launchedPosition);
        State = EState.PostLaunch;

        projectile.Launch((transform.forward * forwardForce) + (transform.up * upForce));
    }

    private void InputReader_OnTouchPressed()
    {
        switch (State)
        {
            case EState.Ready:
                BeginLaunching();
                break;

            case EState.PostLaunch:
                BeginResetting();
                break;
        }

        
        
    }
}
