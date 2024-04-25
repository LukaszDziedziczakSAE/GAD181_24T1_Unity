using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siege_Projectile : MonoBehaviour
{
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] Rigidbody rb;
    [SerializeField] float timeToLive = 30;
    bool launched;
    float timer;

    private void Update()
    {
        if (!launched) return;
        timer += Time.deltaTime;
        if (timer > timeToLive) Destroy(gameObject);
    }

    public void Launch(Vector3 force)
    {
        sphereCollider.enabled = true;
        rb.useGravity = true;
        transform.parent = null;
        rb.velocity = force;
        launched = true;
    }
}
