using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupply_Arrow : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float timeToLive = 15f;
    [SerializeField] LayerMask hittableLayers;
    [SerializeField] float drop = 1f;
    [SerializeField] ParticleSystem iceParticles;
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] Vector3 heightOffSet;
    [SerializeField] int damageAmount = 1; 


    [field: SerializeField] public EType Type {  get; private set; }
    Character owner;
    Character target;

    public enum EType
    {
        none,
        normal,
        fire,
        ice
    }

    float timeAlive = 0;
    bool launched;
    bool hitSomething;
    ArrowSupplyMatch match;

    private void Start()
    {
        match = (ArrowSupplyMatch)Game.Match;
    }

    private void Update()
    {
        if (launched)
        {
            if (target != null) transform.LookAt(target.transform.position + heightOffSet);

            timeAlive += Time.deltaTime;

            //Debug.Log("arrow pos = " + transform.position);

            if (!hitSomething) transform.position += transform.forward * speed * Time.deltaTime;

            if (timeAlive >= timeToLive) Destroy(gameObject);

            /*if (!hitSomething && transform.eulerAngles.x < 90)
            {
                float rotation = transform.eulerAngles.x;
                rotation += drop * Time.deltaTime;
                transform.eulerAngles = new Vector3(rotation, transform.eulerAngles.y, transform.eulerAngles.z);
            }*/
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
            if (!launched) return;

            hitSomething = true;

            Debug.Log("hit");

            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount, Type); // Pass damageAmount when calling TakeDamage
            }
            else
            {
            Debug.Log("No enemy health");
            }
        
        /*TargetShooting_Target target = other.GetComponent<TargetShooting_Target>();
        if (target != null)
        {
            //match.TargetController.RaiseRandomTarget();
            transform.parent = target.transform;
            target.SetDownRoation();
        }*/

    
    }

    public void Launch(Character owner, Character target)
    {
        launched = true;
        this.owner = owner;
        this.target = target;
        StartCoroutine(EnableColliderAfterDelay());
    }

    IEnumerator EnableColliderAfterDelay()
    {
        // Wait for some time before enabling the BoxCollider
        yield return new WaitForSeconds(1.0f); // Change the delay time as needed

        // Enable the BoxCollider
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void SetType(EType newType)
    {
        Type = newType;
        if (Type == EType.fire) fireParticles.gameObject.SetActive(true);
        if (Type == EType.ice) iceParticles.gameObject.SetActive(true);
        Debug.Log("Set arrow type to " + Type.ToString());
    }
}
