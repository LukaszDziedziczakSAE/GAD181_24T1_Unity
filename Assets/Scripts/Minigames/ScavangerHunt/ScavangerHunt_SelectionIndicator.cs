using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerHunt_SelectionIndicator : MonoBehaviour
{
    [SerializeField] float timeToLive = 10f;
    float birthTime;
    float timeAlive => Time.time - birthTime;

    private void OnEnable()
    {
        //Game.InputReader.OnTouchReleased += OnTouchRelease;
        
    }

    private void Start()
    {
        birthTime = Time.time;
    }

    private void Update()
    {
        if (timeAlive >= timeToLive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTouchRelease()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        //Game.InputReader.OnTouchReleased -= OnTouchRelease;
    }
}
