using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour, ISaveable
{
    public object CaptureState()
    {
        return new object();
    }

    public void RestoreState(object state)
    {
    }
}
