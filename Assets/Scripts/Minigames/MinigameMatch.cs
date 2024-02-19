using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MinigameMatch : MonoBehaviour
{
    protected EState mode = EState.none;
    protected float matchTime;

    public EState Mode
    {
        get
        {
            return mode;
        }

        set
        {
            SetNewState(value);
        }
    }

    public float MatchTime => matchTime;

    public enum EState
    {
        none,
        preMatch,
        inProgress,
        postMatch,
        paused
    }

    protected void Start()
    {
        //SetNewState(EState.preMatch);
    }

    protected void Update()
    {
        switch (mode)
        {
            case EState.preMatch: 
                PrematchTick(); break;

            case EState.inProgress:
                MatchTick(); break;

            case EState.postMatch:
                PostMatchTick(); break;
        }
    }

    protected virtual void PrematchStart()
    {
        Mode = EState.inProgress;
    }
    protected abstract void MatchStart();
    protected virtual void PostMatchStart()
    {
        // show postmatch UI
    }

    protected virtual void PrematchEnd() { }
    protected virtual void MatchEnd() { }
    protected virtual void PostMatchEnd()
    {
        Game.LoadMainMenu();
    }

    protected virtual void PrematchTick() { }
    protected virtual void MatchTick()
    {
        matchTime += Time.deltaTime;
    }
    protected virtual void PostMatchTick() { }

    protected abstract MatchResult DetermineResult();

    protected Character[] Compeditors
    {
        get
        {
            List<Character> result = new List<Character>();
            Character[] inLevel = FindObjectsOfType<Character>();
            foreach (Character character in inLevel)
            {
                if (character.PlayerIndex >= 0) result.Add(character);
            }

            return result.ToArray();
        }
    }

    protected void SetNewState(EState newMode)
    {
        switch (mode)
        {
            case EState.preMatch:
                PrematchEnd(); break;

            case EState.inProgress:
                MatchEnd(); break;

            case EState.postMatch:
                PostMatchEnd(); break;
        }

        mode = newMode;

        switch (mode)
        {
            case EState.preMatch:
                PrematchStart(); break;

            case EState.inProgress:
                MatchStart(); break;

            case EState.postMatch:
                PostMatchStart(); break;
        }
    }
}
