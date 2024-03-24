using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_ArrowLauncher : MonoBehaviour
{
    [SerializeField] TargetShooting_Arrow arrowPrefab;
    
    [SerializeField] Character owner;

    float lastIntakePower;
    bool sheduleFiring;
    int errorIteration;

    private void Awake()
    {
        if (owner == null) owner = GetComponent<Character>();
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if (sheduleFiring)
        {
            if (transform.position.y < 0)
            {
                Debug.LogWarning(owner.name + ": Fire Point below zero (" + ++errorIteration + ")");
            }
            else
            {
                Debug.LogWarning(owner.name + ": Fire Point error resolved after " + ++errorIteration + " frames");
                Fire();
                sheduleFiring = false;
                errorIteration = 0;
            }
        }
    }

    public void FireArrow(float intakePower)
    {
        lastIntakePower = intakePower;
        if (transform.position.y < 0)
        {
            Debug.LogWarning(owner.name + ": Fire Point below zero");
            sheduleFiring = true;
        }
        else Fire();
    }

    private void Fire()
    {
        TargetShooting_Arrow arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        if (arrow.transform.position.y < 0) Debug.LogWarning(owner.name + ": fired arrow from spawn position " + arrow.transform.position);
        arrow.Initilise(lastIntakePower, owner);
        
    }
}
