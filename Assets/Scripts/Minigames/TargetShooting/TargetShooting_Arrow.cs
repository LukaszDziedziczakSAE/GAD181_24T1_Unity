using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_Arrow : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float timeToLive = 15f;
    [SerializeField] LayerMask hittableLayers;

    float birthTime;
    float timeAlive => Time.time - birthTime;

    private void Start()
    {
        birthTime = Time.time;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (timeAlive >= timeToLive) Destroy(gameObject);
    }
}
