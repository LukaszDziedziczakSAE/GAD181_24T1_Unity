using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jousting_Weapon : MonoBehaviour
{
    /*[SerializeField] private Character enemyCharacter;

    public bool hasCollided = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == enemyCharacter.gameObject)
        {
            hasCollided = true;
            Debug.Log("Weapon has hit the enemy");
        }
    }*/
    [SerializeField] Collider _collider;
    [SerializeField] Character owner;

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
    }
}
