using IKVM.Reflection;
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
    [field: SerializeField] public Transform RightHand { get; private set; }
    [field: SerializeField] public Transform LeftHand { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    /*[field: SerializeField]*/ public CharacterState State { get; private set; }
    [field: SerializeField] public Animator HorseAnimator { get; private set; }
    [field: SerializeField] public CharacterSounds Sounds { get; private set; }
    [field: SerializeField] public Footsteps Footsteps { get; private set; }

    private void Start()
    {
        if (PlayerIndex >= 100) return;
        if (PlayerIndex != 0) Model.SetNewConfig(StartingConfig);
        //else Model.HideAllModels();
    }

    private void Update()
    {
        if (State != null) State.Tick();

        if (transform.position.y < 0) Debug.LogError(name + " height below 0");
    }

    private void FixedUpdate()
    {
        if (State != null) State.FixedTick();
    }

    public void SetNewState(CharacterState newState)
    {
        /*if (PlayerIndex == 0 || PlayerIndex == 101) */Debug.Log(name + ": Setting new state " + newState.GetType().ToString());
        if (State != null) State.StateEnd();
        State = newState;
        State.StateStart();
    }

    public static System.Type ScavangerLocomotionType => (new CS_ScavangerLocomotion(Game.PlayerCharacter)).GetType();

    public void SetToEnemyInArrowSupply()
    {
        PlayerIndex = 102;
    }
}
