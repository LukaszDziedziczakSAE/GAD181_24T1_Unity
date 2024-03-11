using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_ArrowLauncher : MonoBehaviour
{
    [SerializeField] TargetShooting_Arrow arrowPrefab;
    [SerializeField] Transform firePoint;

    public void FireArrow()
    {
        Debug.Log("Firing arrow");
        TargetShooting_Arrow arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}
