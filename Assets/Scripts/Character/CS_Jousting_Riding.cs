using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Riding : CharacterState
{
    private JoustingMatch match;
    private Character other;
    private UI_Jousting ui;

    public CS_Jousting_Riding(Character character) : base(character)
    {
        match = (JoustingMatch)Game.Match;
        other = match.OtherCharacter(character.PlayerIndex);
        ui = (UI_Jousting)Game.UI;
    }

    public override void StateStart()
    {
        if (character.PlayerIndex == 0)
        {
            ui.JoustingIndicator.gameObject.SetActive(true);
        }
        
        else if (character.PlayerIndex == 1)
        {
            ui.EnemyJoustingIndicator.gameObject.SetActive(true);
        }
        
        //Debug.Log("You've entered riding state");
    }

    public override void Tick()
    {
        character.transform.position += character.transform.forward * match.HorseSpeed * Time.deltaTime;

        if (character.PlayerIndex == 0 )
        {
            ui.JoustingIndicator.UpdateDistanceIndicator(Distance());
            ui.JoustingIndicator.UpdateStrikingDistanceIndicator(IsWithinJoustingDistance());
            ui.EndIndicator.UpdateEndIndicator(ReachedEnd());
            match.PlayerReachedEnd(character);
        }

        else if (character.PlayerIndex == 1 ) 
        {
            ui.EnemyJoustingIndicator.UpdateDistanceIndicator(Distance());
            ui.EnemyJoustingIndicator.UpdateStrikingDistanceIndicator(IsWithinJoustingDistance());
        }
    }

    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {
        if (character.PlayerIndex == 0)
        {
            ui.JoustingIndicator.gameObject.SetActive(false);
        }

        else if (character.PlayerIndex == 1)
        {
            ui.EnemyJoustingIndicator.gameObject.SetActive(false);
        }
    }

    private float Distance()
    {
        float distance = 0;

        if (character.PlayerIndex == 0)
        {
            distance = other.transform.position.z - character.transform.position.z;
        }

        else if (character.PlayerIndex == 1)
        {
            distance = character.transform.position.z - other.transform.position.z;
        }

        if (distance < 0)
        {
            distance = 0;
        }
        
        return distance;
    }

    public bool IsWithinJoustingDistance()
    {
        float distance = Distance();
        return distance >= match.MinimumJoustingDistance && distance <= match.MaximumJoustingDistance;
    }

    private float PlayerPosition()
    {
        float position = 0;
        
        if (character.PlayerIndex == 0)
        {
            position = character.transform.position.z;
        }

        return position;
    }

    public bool ReachedEnd()
    {
        float position = PlayerPosition();
        return position >= match.EndDistance;
    }
}
