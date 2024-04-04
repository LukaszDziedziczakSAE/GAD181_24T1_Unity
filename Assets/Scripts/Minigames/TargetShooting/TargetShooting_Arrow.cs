using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_Arrow : MonoBehaviour
{
    [SerializeField] float baseSpeed = 10;
    [SerializeField] float timeToLive = 5f;
    [SerializeField] LayerMask hittableLayers;
    [SerializeField] float drop = 1f;
    [SerializeField] int pointPerTargetHit = 1;
    [SerializeField] bool dropEnabled;
    float power = 1f;
    float birthTime;
    float timeAlive => Time.time - birthTime;
    bool hitSomething;
    //ArrowShootingMatch match;
    bool errored;
    float speed => baseSpeed * power;
    Character owner;
    ArrowShootingMatch match => (ArrowShootingMatch)Game.Match;

    private void Start()
    {
        name += Random.Range(100, 1000);
        //Debug.Log("Arrow Spawned");
        birthTime = Time.time;
        //match = (ArrowShootingMatch)Game.Match;
    }

    private void Update()
    {
        if (!hitSomething) transform.position += transform.forward * speed * Time.deltaTime;

        if (timeAlive >= timeToLive) Destroy(gameObject);

        if (dropEnabled && !hitSomething && transform.eulerAngles.x < 90)
        {
            float rotation = transform.eulerAngles.x;
            rotation += drop * Time.deltaTime;
            transform.eulerAngles = new Vector3(rotation, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else if (dropEnabled && !hitSomething && transform.eulerAngles.x > 180)
        {
            float rotation = transform.eulerAngles.x;
            rotation += drop * Time.deltaTime;
            transform.eulerAngles = new Vector3(rotation, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        if (!errored && transform.position.y < 0 && !hitSomething)
        {
            errored = true;
            Debug.LogError(name + " below ground " + transform.position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        hitSomething = true;

        TargetShooting_Target target = other.GetComponent<TargetShooting_Target>();
        if (target != null)
        {
            
            transform.parent = target.transform;
            if(target.State == TargetShooting_Target.EState.upPoisition)
            {
                target.StartRotatingDown();

                match.AwardPlayerPoints(owner.PlayerIndex, pointPerTargetHit);

            }
        }

        float distance = Vector3.Distance(transform.position, owner.transform.position);
        //Debug.Log(name + " landed " + distance + " away");

    }

    public void Initilise(float newPowerValue, Character character)
    {
        transform.parent = null;
        owner = character;
        power = newPowerValue;
        Debug.Log(owner.name + " fired arrow with " + (power*100).ToString("F0") + "% power");
    }


}
