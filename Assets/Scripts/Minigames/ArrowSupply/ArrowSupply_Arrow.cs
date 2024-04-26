using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupply_Arrow : MonoBehaviour
{
    [SerializeField] float speed = 10;

    [SerializeField] float timeToLive = 15f;

    [SerializeField] LayerMask hittableLayers;

    // [SerializeField] float drop = 1f;

    [SerializeField] ParticleSystem iceParticles;

    [SerializeField] ParticleSystem fireParticles;

    [SerializeField] Vector3 heightOffSet;

    [SerializeField] int damageAmount = 1;

    [SerializeField] int pointsAdded = 10;

    [SerializeField] float timeToLiveAfterHit = 0.5f;

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip[] arrowSounds;

    [field: SerializeField] public EType Type { get; private set; }

    [SerializeField]  Character owner;

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

    ArrowSupply_Match match;

    private void Start()
    {
        match = (ArrowSupply_Match)Game.Match;
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

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!launched || hitSomething) return;

        //Debug.Log(owner.name + "'s arrow hit " + other.name);


        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            Character character = other.GetComponent<Character>();

            int damageAmount = match.DamageByType(character.Model.CurrentConfig.Variant, Type);

            enemyHealth.TakeDamage(damageAmount); // Pass damageAmount when calling TakeDamage

            hitSomething = true;
            
            match.AwardPlayerPoints(owner.PlayerIndex, match.PointsByDamage(damageAmount));

            timeToLive = timeToLiveAfterHit;
        }
        else
        {
            Debug.Log(name + " hit something with No enemy health");
        }
    }


    public void Launch(Character owner, Character target)
    {
        launched = true;

        this.owner = owner;

        this.target = target;

        PlayArrowFireSound();

        StartCoroutine(EnableColliderAfterDelay());
    }

    IEnumerator EnableColliderAfterDelay()
    {
        yield return new WaitForSeconds(1.0f);

        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void SetType(EType newType)
    {
        Type = newType;

        if (Type == EType.fire) fireParticles.gameObject.SetActive(true);

        if (Type == EType.ice) iceParticles.gameObject.SetActive(true);

        //Debug.Log("Set arrow type to " + Type.ToString());
    }

    public void UpdateScore()
    {
        if (owner.PlayerIndex == 0)
        {
            match.AwardPlayerPoints(0, pointsAdded);
        }
        else if (owner.PlayerIndex == 1)
        {
            match.AwardPlayerPoints(1, pointsAdded);
        }
        else if (owner.PlayerIndex == 2)
        {
            match.AwardPlayerPoints(2, pointsAdded);
        }
        else if (owner.PlayerIndex == 3)
        {
            match.AwardPlayerPoints(3, pointsAdded);
        }
        else
        {
            return;
        }

    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (audioSource == null) return;

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayArrowFireSound()
    {
        if (arrowSounds.Length == 0) return;

        PlayAudioClip(arrowSounds[Random.Range(0, arrowSounds.Length)]);
    }
}

