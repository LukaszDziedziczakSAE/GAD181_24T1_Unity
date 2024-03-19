using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoustingMatch : MinigameMatch
{
    [field: SerializeField] public float HorseSpeed { get; private set; }
    [field: SerializeField] public float MinimumJoustingDistance { get; private set; }
    [field: SerializeField] public float MaximumJoustingDistance { get; private set; }
    [field: SerializeField] public float EndDistance { get; private set; }
    [field: SerializeField] public float PlayerStartPosition { get; private set; }
    [field: SerializeField] public float EnemyStartPosition { get; private set; }
    [field: SerializeField] public float TurnSpeed { get; private set; }
    [field: SerializeField] public float PauseTimerDuration { get; private set; }

    private Jousting_Weapon weapon;
    private Coroutine startTimerCoroutine;

    private int completedRounds = 0;

    public float horseSpeed = 2f;

    private bool isPaused = false;



    protected override MatchResult DetermineResult()
    {
        return new MatchResult();
    }

    protected override void MatchStart()
    {
        weapon = FindObjectOfType<Jousting_Weapon>();
        StartRoundTimer(); // Start the round timer
    }

    private void StartRoundTimer()
    {
        StartCoroutine(RoundTimer());
    }

    private IEnumerator RoundTimer()
    {
        // Set both players to idle state and pause the game
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Jousting_Idle(character));
        }
        isPaused = true;
        yield return new WaitForSeconds(PauseTimerDuration);

        // Resume the game and set both players to riding state
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Jousting_Riding(character));
        }
        isPaused = false;
    }

    protected override void MatchTick()
    {
        base.MatchTick();

        if (completedRounds >= 3)
        {
            Mode = EState.postMatch;
        }
    }

    private IEnumerator StartTimerCoroutine()
    {
        // Set both players to idle state and pause the game
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Jousting_Idle(character));
        }
        isPaused = true;
        yield return new WaitForSeconds(PauseTimerDuration);

        // Resume the game and set both players to riding state
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Jousting_Riding(character));
        }
        isPaused = false;
    }


    public Character OtherCharacter(Character character)
    {
        if (character == null) Debug.LogError("Missing character reference");

        if (character.PlayerIndex == 0)
        {
            return Game.CharacterByIndex(0);
        }
        else if (character.PlayerIndex == 1)
        {
            return Game.CharacterByIndex(1);
        }

        return null;
    }

    public void PlayerReachedEnd(Character character)
    {
        if (character.transform.position.z >= EndDistance)
        {
            character.SetNewState(new CS_Jousting_Idle(character));
            RestartRound();
            completedRounds++;
            Debug.Log("Player Reached End");
        }
    }

    private void RestartRound()
    {
        if (startTimerCoroutine != null)
        {
            StopCoroutine(startTimerCoroutine);
        }

        startTimerCoroutine = StartCoroutine(StartTimerCoroutine());

        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Jousting_Riding(character));
            Vector3 newPosition = character.transform.position;
            if (character.PlayerIndex == 0)
            {
                newPosition.z = PlayerStartPosition;
            }    
            else if (character.PlayerIndex == 1)
            {
                newPosition.z = EnemyStartPosition;
            }
            
            character.transform.position = newPosition;

            Debug.Log(completedRounds);
            //implement restart when player enters impact state
        }
    }
}