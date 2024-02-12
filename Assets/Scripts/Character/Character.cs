using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [field: SerializeField, Header("Settings")] public int PlayerIndex { get; private set; } = -1;
    [field: SerializeField, Header("Settings")] public CharacterModel.Config StartingConfig { get; private set; }
    [field: SerializeField, Header("Referances")] public CharacterModel Model { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CapsuleCollider CapsuleCollider { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }


    private void Start()
    {
        //if (this != Game.PlayerCharacter) Model.SetNewConfig(StartingConfig);
    }
}
