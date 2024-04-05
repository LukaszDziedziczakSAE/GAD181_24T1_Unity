using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jousting_Weapon : MonoBehaviour
{
    [SerializeField] Collider _collider;
    [SerializeField] Character owner;

    JoustingMatch match => (JoustingMatch)Game.Match;
    //private Character character;
    private int pointsToAward = 1;

    public void SetOwner(Character newOwner)
    {
        owner = newOwner;
    }

    public void ColiderEnabled(bool enabled)
    {
        _collider.enabled = enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character otherCharacter = other.GetComponent<Character>();
        if (otherCharacter == null || otherCharacter == owner) return;
        Debug.Log("Hit " + otherCharacter.name);

        otherCharacter.SetNewState(new CS_Jousting_Impact(otherCharacter));
       
        match.AwardPlayerPoints(owner.PlayerIndex, pointsToAward);
    }
}