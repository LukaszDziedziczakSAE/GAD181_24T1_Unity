using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    protected Character character;

    protected void Awake()
    {
        character = GetComponentInParent<Character>();
    }
}
