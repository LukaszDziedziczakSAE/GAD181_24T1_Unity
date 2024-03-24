using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_ArcherFiring : CharacterState
{
    ArrowSupply_Arrow.EType arrowType;

    ArrowSupply_ArrowFirePoint firePoint;
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;

    public CS_ArrowSupply_ArcherFiring(Character character, ArrowSupply_Arrow.EType arrowType) : base(character)
    {
        this.arrowType = arrowType;
    }

    public override void StateStart()
    {
        firePoint = character.GetComponentInChildren<ArrowSupply_ArrowFirePoint>();

        firePoint.FireArrow(arrowType, null, closetEnemy);
    }

    public override void Tick()
    {
        character.SetNewState(new CS_ArrowSupply_ArcherWaiting(character));
    }

    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {

    }

    private Character closetEnemy
    {
        get
        {
            Character closetEnemy = null;

            float closestDistance = Mathf.Infinity;

            foreach (Character enemy in match.AS_Enemies)
            {
                float distance = Vector3.Distance(character.transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closetEnemy = enemy;

                    closestDistance = distance;
                }
            }
            return closetEnemy;
        }
    }
}
