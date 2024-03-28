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
    [field: SerializeField] public float PlayerStartRotation { get; private set; }
    [field: SerializeField] public float EnemyStartPosition { get; private set; }
    [field: SerializeField] public float EnemyStartRotation { get; private set; }
    [field: SerializeField] public float TurnSpeed { get; private set; }

    private Jousting_Weapon weapon;

    private int completedRounds = 0;
    

    protected override void MatchStart()
    {
        weapon = FindObjectOfType<Jousting_Weapon>();

        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Jousting_Idle(character));
        }
    }

    protected override void MatchTick()
    {
        base.MatchTick();

        if (completedRounds >= 3)
        {
            Mode = EState.postMatch;
        }
    }

    public Character OtherCharacter(Character character)
    {
        if (character == null) Debug.LogError("missing character referance");

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
            character.SetNewState(new CS_Jousting_Riding(character));
            RestartRound();
            completedRounds++;
            //Debug.Log("Player Reached End");
        }
    }

    private void RestartRound()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_Jousting_Idle(character));

            Vector3 newPosition = character.transform.position;
            Transform horsePosition = character.transform.Find("JoustingHorse");
            if (character.PlayerIndex == 0)
            {
                newPosition.z = PlayerStartPosition;

                Transform horse = character.transform.Find("JoustingHorse");
                if (horse != null)
                {
                    horse.rotation = Quaternion.identity;
                    horse.localPosition = new Vector3(0f, -0.324f, -0.157f);
                }
            }
            else if (character.PlayerIndex == 1)
            {
                newPosition.z = EnemyStartPosition;

                Transform horse = character.transform.Find("JoustingHorse"); 
                if (horse != null)
                {
                    horse.rotation = Quaternion.Euler(0f, EnemyStartRotation, 0f);
                    horse.localPosition = new Vector3(0f, -0.324f, -0.157f);
                }
            }
            character.transform.position = newPosition;
        }
    }


}