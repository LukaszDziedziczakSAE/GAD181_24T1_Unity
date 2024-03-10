using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jousting_Weapon : MonoBehaviour
{
    [SerializeField] private Character enemyCharacter;

    public bool hasCollided = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == enemyCharacter.gameObject)
        {
            hasCollided = true;
            Debug.Log("Weapon has hit the enemy");
        }
    }
}
