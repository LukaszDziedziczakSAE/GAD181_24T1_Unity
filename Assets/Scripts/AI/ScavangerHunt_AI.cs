using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScavangerHunt_AI : AI
{
    [SerializeField, Header("Settings")] float sphereCastRange;
    [SerializeField] LayerMask pickUpLayer;
    [field: SerializeField, Header("DEBUG")] public ScavangerHunt_PickUp TargetPickUp {  get; private set; }

    private void Update()
    {
        if (Game.Match == null) return;
        if (Game.Match.Mode != MinigameMatch.EState.inProgress) return;
        if (character.State.GetType() != Character.ScavangerLocomotionType) return;

        if (TargetPickUp == null) FindTargetPickUp();

        if (TargetPickUp != null && TargetPickUp.transform.position != character.NavMeshAgent.destination) 
            character.NavMeshAgent.SetDestination(TargetPickUp.transform.position);
    }

    private bool OtherAIHasTarget(ScavangerHunt_PickUp pickUp)
    {
        foreach (Character character in Game.Match.Compeditors)
        {
            ScavangerHunt_AI ai = character.GetComponentInChildren<ScavangerHunt_AI>();
            if (ai != null && ai.TargetPickUp == pickUp) return true;
        }

        return false;
    }

    private void FindTargetPickUp()
    {
        List<RaycastHit> hits = Physics.SphereCastAll(transform.position, sphereCastRange, transform.up, sphereCastRange, pickUpLayer).ToList();
        print(character.name + ": found " + hits.Count + " pickups in range");
        while (TargetPickUp == null || hits.Count > 0)
        {
            RaycastHit closestHit = ClosestHit(hits.ToArray());
            hits.Remove(closestHit);
            ScavangerHunt_PickUp pickUp = closestHit.collider.GetComponent<ScavangerHunt_PickUp>();
            if (pickUp == null) continue;
            if (!OtherAIHasTarget(pickUp)) TargetPickUp = pickUp;
        }
    }

    private RaycastHit ClosestHit(RaycastHit[] hits)
    {
        RaycastHit closestHit = new RaycastHit();
        float closestDistance = Mathf.Infinity;

        foreach (RaycastHit hit in hits)
        {
            float distance = Vector3.Distance(transform.position, hit.point);
            if (distance < closestDistance)
            {
                closestHit = hit;
                closestDistance = distance;
            }
        }

        return closestHit;
    }
}
