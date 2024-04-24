using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DangerTargetShooting_Match : MinigameMatch
{

    [field: SerializeField] public float MinimumDrawDistanceToFire { get; private set; }
    [field: SerializeField] public float MaximumDrawDistanceToFire { get; private set; }
    [field: SerializeField] public TargetShooting_TargetController TargetController { get; private set; }

    [SerializeField] float matchLength = 45f;
    [SerializeField] CinemachineVirtualCamera characterChaseCam;
    [SerializeField] CinemachineVirtualCamera preMatchCam;
    [SerializeField] int maxEasyLevel = 7;
    [SerializeField] int maxMediumLevel = 15;
    [SerializeField] int maxHardLevel = 19;
    [SerializeField] bool displayDebugs;
    public float MatchTimeRemaining => matchLength - MatchTime;
    public bool DisplayDebugs => displayDebugs;

    public enum EDifficulty
    {
        Easy,
        Medium,
        Hard,
        VeryHard
    }
    protected override void PrematchStart()
    {
        base.PrematchStart();
        Game.CameraManager.SetStartingCamera(preMatchCam);
        TargetController.LowerAllTargets();
    }

    protected override void PrematchTick()
    {
        base.PrematchTick();
        if (matchTime > -2.5f && !Game.CameraManager.IsCurrentCamera(characterChaseCam))
        {
            Game.CameraManager.SwitchTo(characterChaseCam, 2f);
        }
    }
    protected override void MatchStart()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Archering_Standing(character));
        }
        foreach (Character character in ClappingAudience)
        {
            character.SetNewState(new CS_Crowd(character));
        }
        foreach (Character character in CheeringAudience)
        {
            character.SetNewState(new CS_Crowd(character));
        }


        TargetController.RaiseRandomTarget();
        ShowTutorial(0);
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
        RemoveBowUpFromHands();
        base.PostMatchStart();
    }

    private void RemoveBowUpFromHands()
    {
        foreach (Character character in Compeditors)
        {
            Transform[] children = character.GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {

                if (child.tag == "Bow")
                {
                    Destroy(child.gameObject);

                }
            }
        }
    }

    public EDifficulty Difficulty
    {
        get
        {
            if (Game.Player.Level.Level <= maxEasyLevel) return EDifficulty.Easy;
            else if (Game.Player.Level.Level <= maxMediumLevel) return EDifficulty.Medium;
            else if (Game.Player.Level.Level <= maxHardLevel) return EDifficulty.Hard;
            else return EDifficulty.VeryHard;
        }
    }


    public Character[] CheeringAudience
    {
        get
        {
            List<Character> audience = new List<Character>();

            Character[] inLevel = FindObjectsOfType<Character>();

            foreach (Character character in inLevel)
            {
                if (character.PlayerIndex == -1) audience.Add(character);
            }

            return audience.ToArray();
        }
    }

    public Character[] ClappingAudience
    {
        get
        {
            List<Character> audience = new List<Character>();

            Character[] inLevel = FindObjectsOfType<Character>();

            foreach (Character character in inLevel)
            {
                if (character.PlayerIndex == -2) audience.Add(character);
            }

            return audience.ToArray();
        }
    }
}