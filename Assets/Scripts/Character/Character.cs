using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [field: SerializeField, Header("Settings")] public int PlayerIndex { get; private set; } = -1;
    [field: SerializeField] public CharacterModel.Config StartingConfig { get; private set; }
    [field: SerializeField, Header("Referances")] public CharacterModel Model { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CapsuleCollider CapsuleCollider { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    /*[field: SerializeField]*/ public CharacterState State { get; private set; }

    private void Start()
    {
        if (PlayerIndex != 0) Model.SetNewConfig(StartingConfig);
    }

    private void Update()
    {
        if (State != null) State.Tick();
    }

    private void FixedUpdate()
    {
        if (State != null) State.FixedTick();
    }

    public void SetNewState(CharacterState newState)
    {
        Debug.Log(name + ": Setting new state " + newState.GetType().ToString());
        if (State != null) State.StateEnd();
        State = newState;
        State.StateStart();
    }
}
