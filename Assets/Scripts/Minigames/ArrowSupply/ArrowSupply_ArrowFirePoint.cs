using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupply_ArrowFirePoint : MonoBehaviour
{
    [SerializeField] ArrowSupply_Arrow arrowPrefab;

    public void FireArrow( ArrowSupply_ArcherSupply.ArrowRecord arrowRecord, Character target)
    {
        //Debug.Log("Firing arrow");

        ArrowSupply_Arrow arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

        arrow.SetType(arrowRecord.Type);


        arrow.Launch(arrowRecord.Owner, target);
    }
}
