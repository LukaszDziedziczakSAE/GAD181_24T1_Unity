using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MinigameMatch : MonoBehaviour
{
    protected EState mode = EState.none;
    protected float matchTime;
    [field: SerializeField] public MinigameConfig Config {  get; protected set; }
    public MatchResult Result { get; protected set; }

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
                PrematchTick(); 
                break;

            case EState.inProgress:
                MatchTick(); 
                break;

            case EState.postMatch:
                PostMatchTick(); 
                break;
        }
    }

    protected virtual void PrematchStart()
    {
        //if (Game.UI.MatchStart != null) Game.UI.MatchStart.gameObject.SetActive(true);
        Result = new MatchResult(Compeditors.Length);
        Mode = EState.inProgress;
    }
    protected abstract void MatchStart();
    protected virtual void PostMatchStart()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_StandingIdle(character));
        }
        Game.UI.MatchStatus.gameObject.SetActive(false);
        Game.UI.MatchEnd.gameObject.SetActive(true);
        Game.UI.MatchEnd.Initilise(DetermineResult());
    }

    protected virtual void PrematchEnd()
    {
        if (Game.UI.MatchStatus != null) Game.UI.MatchStatus.gameObject.SetActive(true);
    }

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

    protected virtual MatchResult DetermineResult()
    {
        Result.CreateResults();
        return Result;
    }

    public Character[] Compeditors
    {
        get
        {
            List<Character> result = new List<Character>();
            Character[] inLevel = FindObjectsOfType<Character>();
            foreach (Character character in inLevel)
            {
                if (character.PlayerIndex >= 0 && character.PlayerIndex < 100) result.Add(character);
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

    public void AwardPlayerPoints(int playerNumber, int points)
    {
        Result.AwardPointsToPlayer(playerNumber, points);
        Game.UI.UpdateMatchStatus();
    }
}
