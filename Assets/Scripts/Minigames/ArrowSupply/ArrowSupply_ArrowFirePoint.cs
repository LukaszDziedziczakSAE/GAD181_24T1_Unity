using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupply_ArrowFirePoint : MonoBehaviour
{
    [SerializeField] ArrowSupply_Arrow arrowPrefab;

    public void FireArrow(/*ArrowSupply_Arrow.EType arrowType*/ ArrowSupply_ArcherSupply.ArrowRecord arrowRecord, /*Character owner,*/ Character target)
    {
        //Debug.Log("Firing arrow");

        ArrowSupply_Arrow arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

        arrow.SetType(arrowRecord.Type);

        // Check if an owner is provided, otherwise use the last known owner
        //Character effectiveOwner = owner ?? ArrowSupply_Arrow.LastOwner;

        arrow.Launch(arrowRecord.Owner, target);
    }
}
