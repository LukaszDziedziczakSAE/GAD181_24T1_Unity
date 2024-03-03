using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerHunt_SelectionIndicator : MonoBehaviour
{
    private void OnEnable()
    {
        Game.InputReader.OnTouchReleased += OnTouchRelease;
    }

    private void OnTouchRelease()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Game.InputReader.OnTouchReleased -= OnTouchRelease;
    }
}
