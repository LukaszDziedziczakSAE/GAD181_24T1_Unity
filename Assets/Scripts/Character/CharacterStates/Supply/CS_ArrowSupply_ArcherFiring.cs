using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArrowSupply_ArcherFiring : CharacterState
{
    //ArrowSupply_Arrow.EType arrowType;
    ArrowSupply_ArcherSupply.ArrowRecord arrowRecord;

    ArrowSupply_ArrowFirePoint firePoint;
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;

    public CS_ArrowSupply_ArcherFiring(Character character, /*ArrowSupply_Arrow.EType arrowType*/ ArrowSupply_ArcherSupply.ArrowRecord arrowRecord) : base(character)
    {
        this.arrowRecord = arrowRecord;
    }

    public override void StateStart()
    {
        firePoint = character.GetComponentInChildren<ArrowSupply_ArrowFirePoint>();

        firePoint.FireArrow(arrowRecord, closestEnemy);
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


    private Character closestEnemy
    {
        get
        {
            Character closestEnemy = null;

            float closestDistance = Mathf.Infinity;

            foreach (Character enemy in match.AS_Enemies)
            {
                float distance = Vector3.Distance(character.transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestEnemy = enemy;

                    closestDistance = distance;
                }
            }
            return closestEnemy;
        }
    }
}
