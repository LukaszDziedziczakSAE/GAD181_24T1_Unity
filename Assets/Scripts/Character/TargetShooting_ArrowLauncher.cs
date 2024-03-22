using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_ArrowLauncher : MonoBehaviour
{
    [SerializeField] TargetShooting_Arrow arrowPrefab;
    
    [SerializeField] Character owner;

    private void Awake()
    {
        if (owner == null) owner = GetComponent<Character>();
    }
    private void Start()
    {
        
    }

    public void FireArrow(float intakePower)
    {
        Debug.Log(owner.name + " Firing arrow");
        TargetShooting_Arrow arrow = Instantiate(arrowPrefab,transform.position, transform.rotation);
        Debug.Log("spawn position " + arrow.transform.position);
        //arrow.transform.forward *= intakePower;

        arrow.Initilise(intakePower, owner);
        if (arrow.transform.position.y < 0)
        {
            arrow.transform.position=transform.position;
        }

    }
}
