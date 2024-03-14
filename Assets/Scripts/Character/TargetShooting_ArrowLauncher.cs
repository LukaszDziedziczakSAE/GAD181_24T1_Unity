using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_ArrowLauncher : MonoBehaviour
{
    [SerializeField] TargetShooting_Arrow arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] Character owner;

    private void Awake()
    {
        if (owner == null) owner = GetComponent<Character>();
    }

    public void FireArrow(float intakePower)
    {
        Debug.Log("Firing arrow");
        TargetShooting_Arrow arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        //arrow.transform.forward *= intakePower;

        arrow.Initilise(intakePower, owner);

    }
}
