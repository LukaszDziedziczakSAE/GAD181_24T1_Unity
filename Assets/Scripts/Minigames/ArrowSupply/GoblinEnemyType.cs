using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinEnemyType : MonoBehaviour, EnemyType
{ 
    public int NormalDamageMultiplier => 4;
    public int FireDamageMultiplier => 2;
    public int IceDamageMultiplier => 1;
}

