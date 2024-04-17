using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MinigameMatch : MonoBehaviour
{
    protected EState mode = EState.none;
    protected float matchTime;
    [field: SerializeField] public MinigameConfig Config {  get; protected set; }
    [field: SerializeField] public UI_TutorialCard[] TutorialCards { get; protected set; }
    public MatchResult Result { get; protected set; }
    [SerializeField] Podium podiumPrefab;
    [SerializeField] Vector3 podiumPosition;
    [SerializeField] Vector3 podiumRotation;

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
        Result = new MatchResult(Compeditors.Length);

        if (Game.UI.Prematch != null)
        {
            Game.UI.Prematch.gameObject.SetActive(true);
            matchTime = -3.5f;
            //print("Prematch time set = " + matchTime);
        }
        else
        {
            Debug.LogWarning("Prematch UI missing from scene");
            Mode = EState.inProgress;
        }
    }
    protected virtual void PrematchTick()
    {
        matchTime += Time.deltaTime;
        if (matchTime >= 0f) Mode = EState.inProgress;
        //print("Prematch time = " + matchTime);
    }

    protected virtual void PrematchEnd()
    {
        if (Game.UI.Prematch != null) Game.UI.Prematch.gameObject.SetActive(false);
        if (Game.UI.MatchStatus != null) Game.UI.MatchStatus.gameObject.SetActive(true);
    }

    protected abstract void MatchStart();

    protected virtual void MatchTick()
    {
        matchTime += Time.deltaTime;
    }

    protected virtual void MatchEnd()
    {

    }

    protected virtual void PostMatchStart()
    {
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character character in characters)
        {
            character.SetNewState(new CS_StandingIdle(character));
        }
        DetermineResult();

        Game.UI.MatchStatus.gameObject.SetActive(false);

        if (podiumPrefab != null)
        {
            Podium podium = Instantiate(podiumPrefab, podiumPosition, Quaternion.Euler(podiumRotation));
            podium.Initilize(Result);
        }
        else
        {
            ShowPostMatchUI();
        }
    }

    protected virtual void PostMatchTick()
    {

    }

    protected virtual void PostMatchEnd()
    {
        Game.LoadMainMenu();
    }

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

        Debug.Log("Match in " + mode + " mode");
    }

    public void AwardPlayerPoints(int playerNumber, int points)
    {
        Result.AwardPointsToPlayer(playerNumber, points);
        Game.UI.UpdateMatchStatus();
        if (playerNumber == 0) Game.Sound.PlayAwardPointSound();
    }

    public void ShowPostMatchUI()
    {
        Game.UI.MatchEnd.gameObject.SetActive(true);
        Game.UI.MatchEnd.Initilise(Result);
    }

    private UI_TutorialCard TutorialCard(int index)
    {
        foreach (UI_TutorialCard card in  TutorialCards)
        {
            if (card.Index == index) return card;
        }

        Debug.LogError("Did not find Tutorial Card with index = " + index.ToString());
        return null;
    }

    public void ShowTutorial(int tutorialIndex)
    {
        if (HasSeenTutorial(tutorialIndex)) return;
        TutorialCard(tutorialIndex).gameObject.SetActive(true);
    }

    public bool HasSeenTutorial(int tutorialIndex)
    {
        return Game.Player.SeenTutorials.HasSeenTutorial(Config, tutorialIndex);
    }
}
