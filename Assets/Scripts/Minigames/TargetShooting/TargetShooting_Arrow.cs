using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_Arrow : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float timeToLive = 15f;
    [SerializeField] LayerMask hittableLayers;
    [SerializeField] float drop = 1f;

    float birthTime;
    float timeAlive => Time.time - birthTime;
    bool hitSomething;
    ArrowShootingMatch match;

    private void Start()
    {
        Debug.Log("Arrow Spawned");
        birthTime = Time.time;
        match = (ArrowShootingMatch)Game.Match;
    }

    private void Update()
    {
        if (!hitSomething) transform.position += transform.forward * speed * Time.deltaTime;

        if (timeAlive >= timeToLive) Destroy(gameObject);

        if (!hitSomething && transform.eulerAngles.x < 90)
        {
            float rotation = transform.eulerAngles.x;
            rotation += drop * Time.deltaTime;
            transform.eulerAngles = new Vector3(rotation, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        hitSomething = true;

        TargetShooting_Target target = other.GetComponent<TargetShooting_Target>();
        if (target != null)
        {
            match.TargetController.RaiseRandomTarget();
            transform.parent = target.transform;
            target.SetDownRoation();
        }

    }
}
